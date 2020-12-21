using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStonks.SPA.Models
{
    public class FilterQuery
    {
        public string VIN { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public int MinMileage { get; set; }
        public int MaxMileage { get; set; }
        public DateTime? FromCarProductionDate { get; set; }
        public DateTime? ToCarProductionDate { get; set; }
        public FuelType? Fuel { get; set; }
        public ConditionState? Condition { get; set; }
        public int MinHorsepower { get; set; }
        public int MaxHorsepower { get; set; }
        public int MinDisplacement { get; set; }
        public int MaxDisplacement { get; set; }
        public TransmissionTypes? TransmissionType { get; set; }
        public DriveTypes? DriveType { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int GenerationId { get; set; }
    }
}
