﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System.Linq;
using Microsoft.PowerFx.Core.Syntax;
using Xunit;

namespace Microsoft.PowerFx.Core.Tests
{
    public class NamedFormulasTests
    {
        [Theory]
        [InlineData("x=1;y=2;")]
        public void NamedFormulaTest(string script)
        {
            var namedFormula = new NamedFormulas(script);
            Assert.Equal(script, namedFormula.Script);
        }

        [Theory]
        [InlineData("x=1;y=2;")]
        public void EnsureParsedTest(string script)
        {
            var namedFormula = new NamedFormulas(script);
            Assert.True(namedFormula.EnsureParsed());
        }

        [Theory]
        [InlineData("x=")]
        public void EnsureParsedWithErrorsTest(string script)
        {
            var namedFormula = new NamedFormulas(script);
            Assert.False(namedFormula.EnsureParsed());            
        }

        [Theory]
        [InlineData("x=")]
        public void GetParseErrorsTest(string script)
        {
            var namedFormula = new NamedFormulas(script);
            namedFormula.EnsureParsed();

            var errors = namedFormula.GetParseErrors();
            Assert.NotEmpty(errors);
        }

        [Theory]
        [InlineData("x=1;")]
        public void GetParseErrorsNoErrorsTest(string script)
        {
            var namedFormula = new NamedFormulas(script);
            namedFormula.EnsureParsed();

            var errors = namedFormula.GetParseErrors();
            Assert.Empty(errors);
        }

        [Theory]
        [InlineData("x=1;y=2;", "1", "2")]
        public void GetNamedFormulasTest(string script, string expectedX, string expectedY)
        {
            var namedFormula = new NamedFormulas(script);
            namedFormula.EnsureParsed();
            var formulas = namedFormula.GetNamedFormulas();
            formulas.OrderBy(formula => formula.formula.Script);

            Assert.NotNull(formulas);

            Assert.Equal(expectedX, formulas.ElementAt(0).formula.Script);
            Assert.Equal(expectedY, formulas.ElementAt(1).formula.Script);
        }
    }
}