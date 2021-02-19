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
        [Required(ErrorMessage = "Nazwa ogłoszenia jest wymagana")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Pole VIN jest wymagane")]
        public string VIN { get; set; }
        public DateTime FirstRegistrationDate { get; set; }
        [Required(ErrorMessage = "Numer rejestracyjny jest wymagany")]
        public string PlateNumber { get; set; }
        public States State { get; set; }
        [Required(ErrorMessage = "Cena jest wymagana")]
        [Range(1, double.MaxValue, ErrorMessage = "Cena musi być większa od 0")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Przebieg jest wymagany")]
        [Range(1, int.MaxValue, ErrorMessage = "Przebieg musi być większy od 0")]
        public int Mileage { get; set; }
        public DateTime CarProductionDate { get; set; }
        public FuelType Fuel { get; set; }
        public ConditionState Condition { get; set; }
        public int Horsepower { get; set; }
        public int Displacement { get; set; }
        [Required(ErrorMessage = "Lokalizacja jest wymagana")]
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
