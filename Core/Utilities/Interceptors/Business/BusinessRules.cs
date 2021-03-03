﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Result;

namespace Core.Utilities.Interceptors.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic;
                }
            }

            return null;
        }
    }
}