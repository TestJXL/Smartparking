// ============================================================================
// ParkingSpace.cs
// ----------------------------------------------------------------------------
// 表示停车场里的一个车位。
//
// 字段说明：
//   - Pid：车位主键，系统分配
//   - Code：车位编号，例如 "A01"（用户可见，必须唯一）
//   - IsEV：这个车位有没有充电桩
//   - IsBig：这是不是大车位（小车也可以停大车位，大车不能停小车位）
//   - LeaseID：如果车位被租出去了，这里是当前租约 ID；否则为 -1
//   - SessionID：如果车位正被占用，这里是当前停车记录 ID；否则为 -1
//   - MapRow / MapColumn：车位在地图上的行列位置（从 0 开始）
//   - Dest：到目的地的距离排名（数字越小越近/越推荐）
//   - IsActive：车位是否启用（停用的车位不参与分配）
//   - IsFree：派生缓存，true 表示"空闲且没被有效租约绑定"
//
// 注意：Status 属性是"派生"的，不存数据库，而是从 SessionID / LeaseID 推算。
// ============================================================================

using Parking.Core.Enums;

namespace Parking.Core.Models
{
    public sealed class ParkingSpace
    {
        // 车位主键（数据库里是主键）
        public int Pid { get; set; }

        // 车位编号，例如 "A01"。用户可见，必须唯一。
        public string Code { get; set; } = "";

        // 这个车位有没有充电桩
        public bool IsEV { get; set; }

        // 这是不是大车位
        public bool IsBig { get; set; }

        // 如果车位被租出去了，这里是当前租约 ID；否则为 -1
        public int LeaseID { get; set; } = -1;

        // 如果车位正被占用，这里是当前停车记录 ID；否则为 -1
        public int SessionID { get; set; } = -1;

        // 车位在地图上的行号（从 0 开始）
        public int MapRow { get; set; }

        // 车位在地图上的列号（从 0 开始）
        public int MapColumn { get; set; }

        // 到目的地的距离排名。数字越小越推荐（近/方便）。
        // 具体的距离计算由推荐服务处理，这里只存一个预先算好的排名值。
        public int Dest { get; set; }

        // 车位是否启用。停用的车位不参与分配，但依然显示在管理界面。
        public bool IsActive { get; set; } = true;

        // 派生缓存：true 表示"空闲且没被有效租约绑定"。
        // 不要从界面直接设置它，由服务层统一维护（RefreshAvailability 方法）。
        public bool IsFree { get; set; }

        // 派生属性：根据 SessionID / LeaseID 推算车位当前状态。
        // 不存数据库，每次访问时实时计算。
        public SpaceStatus Status
        {
            get
            {
                // 有未结束的停车 → 占用
                if (SessionID != -1) return SpaceStatus.Occupied;

                // 有有效租约但没人停车 → 已租
                if (LeaseID != -1) return SpaceStatus.Leased;

                // 否则 → 空闲
                return SpaceStatus.Available;
            }
        }

        // 浅拷贝。用于把车位对象"脱离"数据库上下文后传给界面，
        // 避免界面误改真实数据。
        public ParkingSpace Copy() => (ParkingSpace)MemberwiseClone();
    }
}