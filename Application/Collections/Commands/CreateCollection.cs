using Application.Common;
using MediatR;

namespace Application.Collections.Commands.CreateCollection;

public record CreateCollectionCommand(string Title) : IRequest<Result<Guid>>;