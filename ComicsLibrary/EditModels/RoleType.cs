﻿using ComicsLibrary.Core;

namespace ComicsLibrary.EditModels
{
    public class RoleType : ObservableObject
    {
        private string _name;
        private bool _checked;

        public string Name { get => _name; set { Set(ref _name, value); } }
        public bool Checked { get => _checked; set { Set(ref _checked, value); } }
    }
}