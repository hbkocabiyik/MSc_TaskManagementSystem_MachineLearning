using Agh.DecisionTree.Algorithm;
using Agh.DecisionTree.Entity;

namespace Agh.DecisionTree.Loading
{
    public interface IDataLoader
    {
        IDomainTree Load(EntityData entityData);
        IDomainTree LoadFromFile(string fileName);
    }
}