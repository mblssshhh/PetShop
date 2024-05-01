using System.ComponentModel.DataAnnotations;

namespace PetShopWeb.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле 'Имя' обязательно для заполнения")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле 'Фамилия' обязательно для заполнения")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Поле 'Отчество' обязательно для заполнения")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Поле 'Телефон' обязательно для заполнения")]
        [RegularExpression(@"^\+\d{11}$", ErrorMessage = "Некорректный формат номера телефона")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Поле 'Email' обязательно для заполнения")]
        [EmailAddress(ErrorMessage = "Некорректный формат Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле 'Пароль' обязательно для заполнения")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
