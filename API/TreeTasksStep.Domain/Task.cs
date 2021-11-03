﻿using System;
using System.Collections.Generic;

namespace TreeTasksStep.Domain
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Step> Steps { get; set; }
    }
}
