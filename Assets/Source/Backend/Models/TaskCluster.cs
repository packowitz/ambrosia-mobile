using System;
using System.Collections.Generic;
using Backend.Models.Enums;

namespace Backend.Models
{
    [Serializable]
    public class TaskCluster
    {
        public long id;
        public string name;
        public TaskCategory category;
        public int sortOrder;
        public List<Task> tasks;
    }
}