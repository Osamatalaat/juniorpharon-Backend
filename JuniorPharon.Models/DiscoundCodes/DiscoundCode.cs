
using JuniorPharon.Models.Enums;
namespace JuniorPharon.Models
{
    public class DiscountCode
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public int? MaxUsage { get; set; }   // عدد مرات الاستخدام
        public int UsedCount { get; set; } = 0;

        public int? MinPeople { get; set; } // أقل عدد أشخاص
        public decimal? MinAmount { get; set; } // أقل سعر عشان الكود يشتغل

        public DiscountType Type { get; set; } // Percentage / Fixed
        public decimal Value { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int? TripId { get; set; }
        public int? PackageId { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
