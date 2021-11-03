using System;
using System.Collections.Generic;

namespace TreeTasksStep.Domain
{
    public class Step
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Step> ChildrenSteps { get; set; }
        public virtual Step ParentStep { get; set; }
        public virtual Task ParentTask { get; set; }
    }
}
