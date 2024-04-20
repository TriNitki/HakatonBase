using MediatR;
using Pkg.UseCases;
using Base.UseCases.Abstractions;
using AutoMapper;
using Base.Core.Providers;
using Base.Contracts.Auth;
using Base.Core.Domain;

namespace Base.UseCases.Commands.Auth.Registration;

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
