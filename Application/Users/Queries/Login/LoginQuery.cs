using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Queries.Login
{
    public class LoginQuery : IRequest<ApplicationUserVm>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class LoginQueryHandler : IRequestHandler<LoginQuery, ApplicationUserVm>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly SignInManager<ApplicationUser> _signInManager;

            public LoginQueryHandler(
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager)
            {
                _userManager = userManager;
                _signInManager = signInManager;
            }

            public async Task<ApplicationUserVm> Handle(LoginQuery request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user is null)
                    throw new NotFoundException(nameof(ApplicationUser), request.Email);

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded)
                {
                    // TODO: generate token
                    var vm = new ApplicationUserVm {User = user};
                    return vm;
                }

                throw new Exception("Invalid credentials");
            }
        }
    }
}
