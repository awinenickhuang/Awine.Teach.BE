#pragma warning disable 1591

namespace Awine.IdpCenter.Entities.IdpEntities
{
    public class ApiResourceClaim : UserClaim
    {
        public int ApiResourceId { get; set; }

        public ApiResource ApiResource { get; set; }
    }
}