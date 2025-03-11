using Shouldly;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace FluentModels.UnitTests
{
    [Entity("7311f284-ae1d-4d26-96f5-5e37f7a66c3e")]
    internal class EntityA
    {
        [Member(1)] public int Id { get; set; }
    }

    [Entity("d08e4ec2-917c-4bef-a6cf-133435e667ca", ItemState.Hidden, "Reserved")]
    internal class EntityB
    {
        [Member(1)] public int Id { get; set; }
    }

    public class AttributeTests
    {
        private EntityAttribute GetEntityAttribute(Type type)
        {
            Attribute customAttribute = type.GetCustomAttributes().Single();
            customAttribute.ShouldBeOfType<EntityAttribute>();
            return (EntityAttribute)customAttribute;
        }

        [Fact]
        public void Entity01_Active()
        {
            EntityAttribute entityAttribute = GetEntityAttribute(typeof(EntityA));
            entityAttribute.UniqueId.ShouldBe(new Guid("7311f284-ae1d-4d26-96f5-5e37f7a66c3e"));
            entityAttribute.State.ShouldBe(ItemState.Active);
            entityAttribute.Reason.ShouldBeNull();
        }

        [Fact]
        public void Entity02_Hidden()
        {
            EntityAttribute entityAttribute = GetEntityAttribute(typeof(EntityB));
            entityAttribute.UniqueId.ShouldBe(new Guid("d08e4ec2-917c-4bef-a6cf-133435e667ca"));
            entityAttribute.State.ShouldBe(ItemState.Hidden);
            entityAttribute.Reason.ShouldBe("Reserved");
        }
    }
}
