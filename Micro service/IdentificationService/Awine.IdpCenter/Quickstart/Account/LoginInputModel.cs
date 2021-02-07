using System.ComponentModel.DataAnnotations;

namespace IdentityServerHost.Quickstart.UI
{
    public class LoginInputModel
    {
        /// <summary>
        /// ÕËºÅ
        /// </summary>
        [Required(ErrorMessage = "ÕËºÅ±ØÌî"), MaxLength(18, ErrorMessage = "ÕËºÅÌ«³¤À²"), MinLength(6, ErrorMessage = "ÕËºÅÌ«¶ÌÀ²")]
        public string Account { get; set; }

        /// <summary>
        /// ÃÜÂë
        /// </summary>
        [Required(ErrorMessage = "ÃÜÂë±ØÌî"), MaxLength(18, ErrorMessage = "ÃÜÂëÌ«³¤À²"), MinLength(6, ErrorMessage = "ÃÜÂëÌ«¶ÌÀ²")]
        public string Password { get; set; }

        public bool RememberLogin { get; set; }

        public string ReturnUrl { get; set; }
    }
}