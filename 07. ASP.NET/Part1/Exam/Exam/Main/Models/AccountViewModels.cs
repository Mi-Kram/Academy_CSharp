using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Main.Models
{
  public class ExternalLoginConfirmationViewModel
  {
    [Required]
    [Display(Name = "Адрес электронной почты")]
    public string Email { get; set; }
  }

  public class ExternalLoginListViewModel
  {
    public string ReturnUrl { get; set; }
  }

  public class SendCodeViewModel
  {
    public string SelectedProvider { get; set; }
    public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    public string ReturnUrl { get; set; }
    public bool RememberMe { get; set; }
  }

  public class VerifyCodeViewModel
  {
    [Required]
    public string Provider { get; set; }

    [Required]
    [Display(Name = "Код")]
    public string Code { get; set; }
    public string ReturnUrl { get; set; }

    [Display(Name = "Запомнить браузер?")]
    public bool RememberBrowser { get; set; }

    public bool RememberMe { get; set; }
  }

  public class ForgotViewModel
  {
    [Required]
    [Display(Name = "Адрес электронной почты")]
    public string Email { get; set; }
  }

  public class LoginViewModel
  {
    [Required]
    [Display(Name = "Логин")]
    public string Login { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }


    public string url { get; set; } = "";
	}

  public class RegisterViewModel
  {
    [Required]
    [Display(Name = "Фото")]
    public System.Web.HttpPostedFileBase Photo { get; set; }

    [Required]
    [Display(Name = "Логин")]
    [MaxLength(15, ErrorMessage = "Логин должен состоять не более, чем из 15 символов!")]
    [MinLength(3, ErrorMessage = "Слишком короткий логин!")]
    public string Login { get; set; }

    [Required]
    [MaxLength(15, ErrorMessage = "Пароль должен состоять не более, чем из 15 символов!")]
    [MinLength(6, ErrorMessage = "Слишком короткий пароль!")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Подтверждение пароля")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
    public string ConfirmPassword { get; set; }

    public string url { get; set; } = "";
  }

  public class ResetPasswordViewModel
  {
    [Required]
    //[EmailAddress]
    [Display(Name = "Адрес электронной почты")]
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Подтверждение пароля")]
    [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
    public string ConfirmPassword { get; set; }

    public string Code { get; set; }
  }

  public class ForgotPasswordViewModel
  {
    [Required]
    //[EmailAddress]
    [Display(Name = "Почта")]
    public string Email { get; set; }
  }
}
