using System.Collections.Generic;
using TreeTaskStep.TreeData;

namespace TreeTaskStep.Services
{
    public interface IHierarchyBuilder<T>
    {
        public ITree<T> Tree { get; set; }

        public List<ITree<T>> GetRootLevelSteps();
        public List<ITree<T>> GetFlattenedListOfNodes();
    }
}
