namespace Agh.DecisionTree.Entity
{
    public interface IEntityDataValidator
    {
        Outcome Validate(EntityData entityData);
    }
}