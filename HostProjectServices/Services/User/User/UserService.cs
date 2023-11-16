using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;

using Microsoft.Extensions.Configuration;

using Services.Model;

using Data.Models;

namespace Services.Services
{
    public class UserService : GenericRepositoryAsync<User>, IUserService
    {
        #region fields
        private readonly hosteduContext _hosteduContext;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public IConfiguration Configuration { get; }

        #endregion

        #region ctor
        public UserService(
            hosteduContext hosteduContext,
            IConfiguration configuration,
            IMapper mapper,
        IUnitOfWork unitOfWork

            ) : base(hosteduContext)
        {

            this._unitOfWork = unitOfWork;
            this._hosteduContext = hosteduContext;
            Configuration = configuration;
            this._mapper = mapper;
        }
        #endregion


        public async Task<List<UserModel>> GetUsers()
        {
            var result = new List<UserModel>();
            var getData = await Task.Run(() => _hosteduContext.User.ToListAsync());
            result = _mapper.Map<List<UserModel>>(getData);
            return result;
        }

        public async Task<string> InsertUser(UserSetModel input)
        {
            if (string.IsNullOrEmpty(input.Email) || input.Email == "")
            {
                return "Please Insert Email Address!";
            }
            else
            {
                var chkEmail = await Task.Run(() => _hosteduContext.User.Where(x => x.Email.ToLower() == input.Email.ToLower()).FirstOrDefault());
                if (chkEmail != null)
                {
                    return "User Not be Inserted beacause of Email Address Already Exist!";
                }
            }
            if (string.IsNullOrEmpty(input.Mobile) || input.Mobile == "")
            {
                return "Please Insert Mobile Number!";
            }
            else
            {
                var chkMobile = await Task.Run(() => _hosteduContext.User.Where(x => x.Mobile == input.Mobile).FirstOrDefault());
                if (chkMobile != null)
                {
                    return "User Not be Update beacause of Mobile Number Already Exist!";
                }
            }
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    User user = await Task.Run(() => _hosteduContext.User.Where(x => x.Name == input.Name).FirstOrDefault());
                    if (user == null)
                    {
                        string passSalt = string.Empty;
                        HashingHelper hashHelper = HashingHelper.GetInstance();
                        string pwdHash = hashHelper.GenerateHash(input.PassHash, ref passSalt);
                        input.PassHash = pwdHash;
                        input.PassSalt = passSalt;
                        var request = await base.AddAsync(_mapper.Map<User>(input));
                        var result = await _unitOfWork.SaveChangesAsync();
                        transaction.Commit();
                        return "User Successfully Inserted!";
                    }
                    else
                    {
                        return "User Already Exist!";
                    }
                }
                catch (Exception ex)
                {
                    var Exception = ex;
                    transaction.Rollback();
                    return "User Inserted Failed!";
                }
            }
        }
    }
}
