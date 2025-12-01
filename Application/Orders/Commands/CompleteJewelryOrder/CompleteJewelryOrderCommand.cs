using Application.Common;
using MediatR;

namespace Application.Orders.Commands;

public record CompleteJewelryOrderCommand(Guid Id, string? CompletionNotes) : IRequest<Result>;