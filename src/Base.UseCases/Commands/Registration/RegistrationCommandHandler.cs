using MediatR;
using Microsoft.Extensions.Options;
using Pkg.UseCases;
using Base.Core;
using Base.Core.Options;
using Base.UseCases.Abstractions;
using Base.UseCases.Commands.Login;
using AutoMapper;
using Base.Core.Providers;
using Base.Contracts.Auth;

namespace Base.UseCases.Commands.Registration;

/// <summary>
/// Обработчик команды авторизации.
/// </summary>
public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, Result<Tokens>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHashProvider _passwordHashProvider;
    private readonly IMapper _mapper;


    public RegistrationCommandHandler(
        IUserRepository userRepository,
        IJwtProvider jwtProvider,
        IPasswordHashProvider passwordHashProvider,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtProvider = jwtProvider;
        _passwordHashProvider = passwordHashProvider;
    }

    public async Task<Result<Tokens>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByLoginAsync(request.Login) != null)
            return Result<Tokens>.Conflict("User with this login already exists");

        var user = _mapper.Map<User>(request);
        await _userRepository.CreateAsync(user);

        string accessToken = _jwtProvider.GenerateAccess(user.Id, user.Nickname);
        RefreshToken refreshToken = await _jwtProvider.GenerateSaveRefreshAsync(user.Id);

        return Result<Tokens>.Success(
            new Tokens()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                RefreshExpiration = refreshToken.Expiration
            });
    }
}
