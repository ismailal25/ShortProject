using ShortProject.Exceptions;
using ShortProject.Models;

namespace ShortProject.Services
{
    public class MedicineService
    {
        public void CreateMedicine(Medicine medicine)
        {
            if (!CategoryExists(medicine.CategoryId))
            {
                throw new NotFoundException($"Category with ID {medicine.CategoryId} not found.");
            }

            Array.Resize(ref DB.Medicines, DB.Medicines.Length + 1);
            DB.Medicines[DB.Medicines.Length - 1] = medicine;
        }

        public List<Medicine> GetAllMedicines()
        {
            return DB.Medicines.ToList();
        }

        public Medicine GetMedicineById(int id)
        {
            Medicine medicine = DB.Medicines.FirstOrDefault(medicine => medicine.Id == id);
            if (medicine == null)
            {
                throw new NotFoundException($"Medicine with ID {id} not found.");
            }
            return medicine;
        }

        public Medicine GetMedicineByName(string name)
        {
            Medicine medicine = DB.Medicines.FirstOrDefault(medicine => medicine.Name == name);
            if (medicine == null)
            {
                throw new NotFoundException($"Medicine with name '{name}' not found.");
            }
            return medicine;
        }

        public List<Medicine> GetMedicineByCategory(int categoryId)
        {
            List<Medicine> medicinesInCategory = DB.Medicines.Where(medicine => medicine.CategoryId == categoryId).ToList();
            return medicinesInCategory;
        }

        public void RemoveMedicine(int id)
        {
            Medicine medicineToRemove = DB.Medicines.FirstOrDefault(medicine => medicine.Id == id);
            if (medicineToRemove == null)
            {
                throw new NotFoundException($"Medicine with ID {id} not found.");
            }

            Medicine[] newMedicines = DB.Medicines.Where(medicine => medicine.Id != id).ToArray();
            DB.Medicines = newMedicines;
        }

        public void UpdateMedicine(int id, Medicine updatedMedicine)
        {
            Medicine medicineToUpdate = DB.Medicines.FirstOrDefault(medicine => medicine.Id == id);
            if (medicineToUpdate == null)
            {
                throw new NotFoundException($"Medicine with ID {id} not found.");
            }

            medicineToUpdate.Name = updatedMedicine.Name;
            medicineToUpdate.Price = updatedMedicine.Price;
            medicineToUpdate.CategoryId = updatedMedicine.CategoryId;
            medicineToUpdate.UserId = updatedMedicine.UserId;
            medicineToUpdate.CreatedDate = updatedMedicine.CreatedDate;
        }

        private bool CategoryExists(int categoryId)
        {
            return DB.Categories.Any(category => category.Id == categoryId);
        }




    }
}
