using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using DNANET.Service;
using DNANET.Data;

namespace NetDLims.Services
{
    public class UserServiceClient : ClientBase<IUserService>
    {
        public UserServiceClient()
            : base("UserService")
        {
        }

        private void P_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {

        }

        public UserInformation Login(string login, string password, ref string msg, ref int code)
        {
            ServiceResult<UserInformation> result = base.Channel.Login(login, password);
            msg = result.Message;
            code = result.Code;
            return result.Data;
        }

        public UserInformation[] FindUserInformation(bool fullQuery,params string[] logins)
        {
            ServiceResult<UserInformation[]> result = base.Channel.FindUserInformation(fullQuery,logins);

            return result.Data;
        }
    }
}
