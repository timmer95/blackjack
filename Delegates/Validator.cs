using System;
using System.Collections.Generic;
using System.Text;

namespace Delegates;

public delegate bool Validator<T>(string input, out T output);
