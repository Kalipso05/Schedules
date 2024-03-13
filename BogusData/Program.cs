using Bogus;
using DataCenter.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BogusData
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                using (var db = new dbModel())
                {

                    var genderOptions = new[] { "Мужской", "Женский" }; // Пример значений пола
                    var passportSeries = new[] { "4505", "4604", "4703", "4802" };
                    var workplaces = new[] {
                        "ООО Рога и Копыта",
                        "ЗАО НаноТех",
                        "ИП Иванов",
                        "МУП Центральная поликлиника",
                        "ОАО СтройГаз"
                    };

                    var diagnoses = new[] {
                        "ОРВИ",
                        "Гастрит",
                        "Бронхит",
                        "Ангина",
                        "Аллергия"
                    };

                    // Генератор для Gender
                    var genders = new Faker<Gender>()
                        .RuleFor(g => g.Title, f => f.PickRandom(genderOptions))
                        .Generate(2);
                    db.Gender.AddRange(genders);
                    db.SaveChanges();

                    // Генератор для Passport
                    var passports = new Faker<Passport>()
                        .RuleFor(p => p.SeriesPassport, f => f.PickRandom(passportSeries))
                        .RuleFor(p => p.NumberPassport, f => f.Random.Number(100000, 999999).ToString())
                        .Generate(100);
                    db.Passport.AddRange(passports);
                    db.SaveChanges();

                    // Генератор для MedicalCard
                    var medicalCards = new Faker<MedicalCard>()
                        .RuleFor(m => m.Number, f => $"MC-{f.Random.Hexadecimal(8)}")
                        .RuleFor(m => m.DateOfIssue, f => f.Date.Past(10))
                        .RuleFor(m => m.DateOfLastApeal, f => f.Date.Past(2))
                        .RuleFor(m => m.DateOfNextApeal, f => f.Date.Soon(30))
                        .Generate(100);
                    db.MedicalCard.AddRange(medicalCards);
                    db.SaveChanges();
                    // Генератор для InsurancePolicy
                    var insurancePolicies = new Faker<InsuransePolicy>()
                        .RuleFor(i => i.Number, f => $"IP-{f.Random.Hexadecimal(8)}")
                        .RuleFor(i => i.DateOfExpiration, f => f.Date.Future(1))
                        .Generate(100);
                    db.InsuransePolicy.AddRange(insurancePolicies);
                    db.SaveChanges();

                    var insuranceCompanyFaker = new Faker<InsuranseCompany>("ru")
                        .RuleFor(i => i.Title, f => f.Company.CompanyName());
                    var insuranceCompanies = insuranceCompanyFaker.Generate(10);
                    db.InsuranseCompany.AddRange(insuranceCompanies);
                    db.SaveChanges();

                    var insuranceCompanyIds = db.InsuranseCompany.Select(ic => ic.ID).ToList();

                    var patientGenerator = new Faker<Patient>("ru")
                        .RuleFor(u => u.Photo, f => f.Image.LoremFlickrUrl())
                        .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                        .RuleFor(u => u.LastName, f => f.Name.LastName())
                        .RuleFor(u => u.Gender, f => f.PickRandom(genders))
                        .RuleFor(u => u.Patronymic, (f, u) =>
                        {
                            var maleMiddleNames = new[] { "Александрович", "Дмитриевич", "Максимович", "Иванович", "Петрович" };
                            var femaleMiddleNames = new[] { "Александровна", "Дмитриевна", "Максимовна", "Ивановна", "Петровна" };
                            return u.Gender.Title == "Мужской" ? f.PickRandom(maleMiddleNames) : f.PickRandom(femaleMiddleNames);
                        })

                        .RuleFor(u => u.DateOfBirth, f => f.Date.Past(80, DateTime.Today.AddYears(-18)))
                        .RuleFor(u => u.Adress, f => f.Address.FullAddress())
                        .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber())
                        .RuleFor(u => u.Email, f => f.Internet.Email())
                        .RuleFor(u => u.WorkPlace, f => f.PickRandom(workplaces))
                        .RuleFor(u => u.Diagnos, f => f.PickRandom(diagnoses))
                        .RuleFor(u => u.IDGender, f => f.PickRandom(genders).ID)
                        .RuleFor(u => u.IDPassport, (f, u) => passports[f.IndexFaker % passports.Count].ID)
                        .RuleFor(u => u.IDMedicalCard, (f, u) => medicalCards[f.IndexFaker % medicalCards.Count].ID)
                        .RuleFor(u => u.IDInsuransePolicy, (f, u) => insurancePolicies[f.IndexFaker % insuranceCompanies.Count].ID)
                        .RuleFor(u => u.IDInsuranseCompany, f => f.PickRandom(insuranceCompanyIds));

                    var patients = patientGenerator.Generate(100);
                    db.Patient.AddRange(patients);
                    db.SaveChanges();

                    Console.WriteLine("Generation complete.");
                }
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessageBuilder = new StringBuilder();
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessageBuilder.AppendLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
                Console.WriteLine(errorMessageBuilder);
            }
        }
    }

}
