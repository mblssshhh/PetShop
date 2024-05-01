using System.ComponentModel.DataAnnotations;

namespace PetShopWeb.Models
{
    public class UserProfileViewModel
    {
        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна для заполнения")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Отчество обязательно для заполнения")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Телефон обязателен для заполнения")]
        [RegularExpression(@"^\+\d{11}$", ErrorMessage = "Некорректный формат номера телефона")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Поле 'Email' обязательно для заполнения")]
        [EmailAddress(ErrorMessage = "Некорректный формат Email")]
        public string Email { get; set; }
    }
}
