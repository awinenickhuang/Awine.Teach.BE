using System.ComponentModel.DataAnnotations;

namespace IdentityServerHost.Quickstart.UI
{
    public class LoginInputModel
    {
        /// <summary>
        /// �˺�
        /// </summary>
        [Required(ErrorMessage = "�˺ű���"), MaxLength(18, ErrorMessage = "�˺�̫����"), MinLength(6, ErrorMessage = "�˺�̫����")]
        public string Account { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [Required(ErrorMessage = "�������"), MaxLength(18, ErrorMessage = "����̫����"), MinLength(6, ErrorMessage = "����̫����")]
        public string Password { get; set; }

        public bool RememberLogin { get; set; }

        public string ReturnUrl { get; set; }
    }
}