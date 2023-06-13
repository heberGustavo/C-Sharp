using RestWithNETUdemy.Data.VO;
using RestWithNETUdemy.Model;

namespace RestWithNETUdemy.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);
        
        User ValidateCredentials(string userName);
        
        User RefreshUserInfo(User user);

        bool RevokeToken(string userName);
    }
}
