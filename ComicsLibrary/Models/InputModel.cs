﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ComicsLibrary.Models
{
    public abstract class InputModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Remark { get; set; }
    }
}
