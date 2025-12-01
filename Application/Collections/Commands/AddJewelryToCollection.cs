using Application.Common;
using MediatR;

namespace Application.Collections.Commands.AddJewelryToCollection;

public record AddJewelryToCollectionCommand(Guid CollectionId, Guid JewelryId) : IRequest<Result>;