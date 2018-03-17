﻿// Copyright (c) 2014 Daniel Grunwald
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System.Collections.Immutable;
using System.Threading;
using ICSharpCode.Decompiler.CSharp.Syntax;
using ICSharpCode.Decompiler.TypeSystem;

namespace ICSharpCode.Decompiler.CSharp.Transforms
{
	/// <summary>
	/// Parameters for IAstTransform.
	/// </summary>
	public class TransformContext
	{
		public readonly DecompilerTypeSystem TypeSystem;
		public readonly CancellationToken CancellationToken;
		public readonly TypeSystemAstBuilder TypeSystemAstBuilder;
		public readonly DecompilerSettings Settings;
		internal readonly DecompileRun DecompileRun;

		readonly ITypeResolveContext decompilationContext;

		/// <summary>
		/// Returns the member that is being decompiled; or null if a whole type or assembly is being decompiled.
		/// </summary>
		public IMember DecompiledMember {
			get { return decompilationContext.CurrentMember; }
		}
		
		/// <summary>
		/// Returns the type definition that is being decompiled; or null if an assembly is being decompiled.
		/// </summary>
		public ITypeDefinition DecompiledTypeDefinition {
			get { return decompilationContext.CurrentTypeDefinition; }
		}

		/// <summary>
		/// Returns the assembly that is being decompiled.
		/// </summary>
		public IAssembly DecompiledAssembly {
			get { return decompilationContext.CurrentAssembly; }
		}

		/// <summary>
		/// Returns the max possible set of namespaces that will be used during decompilation.
		/// </summary>
		public IImmutableSet<string> RequiredNamespacesSuperset => DecompileRun.Namespaces.ToImmutableHashSet();

		internal TransformContext(DecompilerTypeSystem typeSystem, DecompileRun decompileRun, ITypeResolveContext decompilationContext, TypeSystemAstBuilder typeSystemAstBuilder)
		{
			this.TypeSystem = typeSystem;
			this.DecompileRun = decompileRun;
			this.decompilationContext = decompilationContext;
			this.TypeSystemAstBuilder = typeSystemAstBuilder;
			this.CancellationToken = decompileRun.CancellationToken;
			this.Settings = decompileRun.Settings;
		}
	}
}
