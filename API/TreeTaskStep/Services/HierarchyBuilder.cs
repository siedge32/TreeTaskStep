using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TreeTasksStep.Domain;
using TreeTasksStep.Persistance;
using TreeTaskStep.TreeData;

namespace TreeTaskStep.Services
{
    public class HierarchyBuilder : IHierarchyBuilder<Step>
    {
        private readonly DataContext _context;
        public ITree<Step> Tree { get; set; }

        public List<ITree<Step>> GetRootLevelSteps()
        {
            return Tree.Children.ToList();
        }

        public List<ITree<Step>> GetFlattenedListOfNodes()
        {
            return Tree.Children.Flatten(node => node.Children).ToList();
        }

        public HierarchyBuilder(DataContext context)
        {
            _context = context;
            var steps = _context.Steps
                .Include(step => step.ParentStep)
                .Include(step => step.ParentTask)
                .ToList();

            Tree = steps.ToTree((parent, child) => child.ParentStep?.Id == parent.Id);
        }
    }
}
