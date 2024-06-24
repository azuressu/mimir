using Lib9c.GraphQL.Types;
using Mimir.GraphQL.Objects;
using Mimir.GraphQL.Resolvers;
using Nekoyume.Model.EnumType;

namespace Mimir.GraphQL.Types;

public class AvatarType : ObjectType<AvatarObject>
{
    protected override void Configure(IObjectTypeDescriptor<AvatarObject> descriptor)
    {
        descriptor
            .Field(f => f.Address)
            .Type<NonNullType<AddressType>>();
        descriptor
            .Field(f => f.AgentAddress)
            .Type<AddressType>()
            .ResolveWith<AvatarResolver>(_ =>
                AvatarResolver.GetAgentAddress(default!, default!, default!, default!, default!));
        descriptor
            .Field(f => f.Index)
            .Type<IntType>();
        descriptor
            .Field("name")
            .Type<StringType>()
            .ResolveWith<AvatarResolver>(_ =>
                AvatarResolver.GetName(default!, default!, default!, default!, default!));
        descriptor
            .Field("level")
            .Type<IntType>()
            .ResolveWith<AvatarResolver>(_ =>
                AvatarResolver.GetLevel(default!, default!, default!, default!, default!));
        descriptor
            .Field("actionPoint")
            .Type<IntType>()
            .ResolveWith<AvatarResolver>(_ =>
                AvatarResolver.GetActionPoint(default!, default!, default!));
        descriptor
            .Field("dailyRewardReceivedBlockIndex")
            .Type<IntType>()
            .ResolveWith<AvatarResolver>(_ =>
                AvatarResolver.GetDailyRewardReceivedBlockIndex(default!, default!, default!));
        descriptor
            .Field("inventory")
            .Type<InventoryType>()
            .ResolveWith<AvatarResolver>(_ => AvatarResolver.GetInventory(default!));
        descriptor
            .Field("runes")
            .Type<ListType<NonNullType<RuneType>>>()
            .ResolveWith<AvatarResolver>(_ =>
                AvatarResolver.GetRunes(default!, default!, default!));
        descriptor
            .Field("collection")
            .Type<ListType<NonNullType<CollectionElementType>>>()
            .ResolveWith<AvatarResolver>(_ =>
                AvatarResolver.GetCollectionElements(default!, default!, default!));
        descriptor
            .Field("itemSlots")
            .Argument("battleType", a => a.Type<NonNullType<EnumType<BattleType>>>())
            .Type<ItemSlotType>()
            .ResolveWith<AvatarResolver>(_ =>
                AvatarResolver.GetItemSlot(default!, default!, default!, default!));
    }
}
