using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStonks.SPA.Models
{
    public enum States
    {
        New, //Dodane, nieopłacone
        Active, //Dodane, opłacone
        Expired,
        Terminated
    }
    public enum FuelType
    {
        Diesel,
        Petrol,
        Electric,
        LPG
    }
    public enum ConditionState
    {
        Damaged,
        Undamaged
    }
    public enum DriveTypes
    {
        FWD,
        RWD,
        AWD
    }
    public enum TransmissionTypes
    {
        Sequence,
        Automatic,
        Manual
    }

    public class Advert
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public DateTime ExpiryDate { get; set; } //ustawiamy dopiero, gdy otrzymamy płatność
        public bool IsPromoted { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        // [Required(ErrorMessage = "Pole Vin jest wymagane")]
        public string VIN { get; set; }
        public DateTime FirstRegistrationDate { get; set; }
        public string PlateNumber { get; set; }
        public States State { get; set; }
        public double Price { get; set; }
        public int Mileage { get; set; }
        public DateTime CarProductionDate { get; set; }
        public FuelType Fuel { get; set; }
        public ConditionState Condition { get; set; }
        public int Horsepower { get; set; }
        public int Displacement { get; set; }
        public string Location { get; set; }
        public bool HasBeenCrashed { get; set; }
        public TransmissionTypes TransmissionType { get; set; }
        public DriveTypes DriveType { get; set; }
        public int VisitCount { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Pola Marka, Model i Generacja są wymagane.")]
        public int GenerationId { get; set; }
        public string PhoneNumber { get; set; }
        public List<Photo> Photos { get; set; }
        public List<AdvertEquipment> AdvertEquipments { get; set; }
        public Generation Generation { get; set; }
        public User User { get; set; }
        public List<Payment> Payments { get; set; }
    }
}
