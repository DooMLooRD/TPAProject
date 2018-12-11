using DataLayer.DataModel.Enums;

namespace DataLayer.DataModel
{
    public class TypeModifiers
    {
        public AbstractEnum? AbstractEnum { get; set; }
        public AccessLevel? AccessLevel { get; set; }
        public SealedEnum? SealedEnum { get; set; }
        public StaticEnum? StaticEnum { get; set; }
    }
}