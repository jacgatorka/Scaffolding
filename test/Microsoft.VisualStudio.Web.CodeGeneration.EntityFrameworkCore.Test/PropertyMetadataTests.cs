// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore.Test.TestModels;
using Xunit;

namespace Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore.Test
{
    public class PropertyMetadataTests
    {
        [Fact]
        public void Primary_Key_Metadata_Is_Correct()
        {
            //Arrange
            var productEntity = TestModel.CategoryProductModel.FindEntityType(typeof(Product));
            var modelMetadata = new ModelMetadata(productEntity, typeof(TestDbContext));

            //Act
            var propertyMetadata = modelMetadata.Properties.FirstOrDefault(p => p.PropertyName == nameof(Product.ProductId));

            //Assert
            Assert.Equal(nameof(Product.ProductId), propertyMetadata.PropertyName);
            Assert.True(propertyMetadata.IsPrimaryKey);
            Assert.False(propertyMetadata.IsForeignKey);
            Assert.Equal(typeof(int).FullName, propertyMetadata.TypeName);
            Assert.False(propertyMetadata.IsEnum);
            Assert.True(propertyMetadata.IsAutoGenerated);
            Assert.False(propertyMetadata.IsEnumFlags);
            Assert.False(propertyMetadata.IsReadOnly);
            Assert.True(propertyMetadata.Scaffold);

            // Arrange
            var orderEntity = TestModel.CustomerOrderModel.FindEntityType(typeof(Order));
            modelMetadata = new ModelMetadata(orderEntity, typeof(TestDbContext));

            // Act
            propertyMetadata = modelMetadata.Properties.FirstOrDefault(p => p.PropertyName == nameof(Order.OrderId));

            // Assert
            Assert.Equal(nameof(Order.OrderId), propertyMetadata.PropertyName);
            Assert.True(propertyMetadata.IsPrimaryKey);
            Assert.False(propertyMetadata.IsForeignKey);
            Assert.Equal(typeof(int).FullName, propertyMetadata.TypeName);
            Assert.False(propertyMetadata.IsEnum);
            Assert.False(propertyMetadata.IsAutoGenerated);
            Assert.False(propertyMetadata.IsEnumFlags);
            Assert.False(propertyMetadata.IsReadOnly);
            Assert.True(propertyMetadata.Scaffold);
        }

        [Fact]
        public void Foreign_Key_Metadata_Is_Correct()
        {
            //Arrange
            var productEntity = TestModel.CategoryProductModel.FindEntityType(typeof(Product));
            var modelMetadata = new ModelMetadata(productEntity, typeof(TestDbContext));

            //Act
            var propertyMetadata = modelMetadata.Properties.FirstOrDefault(p => p.PropertyName == nameof(Product.CategoryId));

            //Assert
            Assert.Equal(nameof(Product.CategoryId), propertyMetadata.PropertyName);
            Assert.False(propertyMetadata.IsPrimaryKey);
            Assert.True(propertyMetadata.IsForeignKey);
            Assert.Equal(typeof(int).FullName, propertyMetadata.TypeName);
            Assert.False(propertyMetadata.IsEnum);
            Assert.False(propertyMetadata.IsAutoGenerated);
            Assert.False(propertyMetadata.IsEnumFlags);
            Assert.False(propertyMetadata.IsReadOnly);
            Assert.True(propertyMetadata.Scaffold);
        }

        [Fact]
        public void String_Property_Metadata_Is_Correct()
        {
            //Arrange
            var productEntity = TestModel.CategoryProductModel.FindEntityType(typeof(Product));
            var modelMetadata = new ModelMetadata(productEntity, typeof(TestDbContext));

            //Act
            var propertyMetadata = modelMetadata.Properties.FirstOrDefault(p => p.PropertyName == nameof(Product.ProductName));

            //Assert
            Assert.Equal(nameof(Product.ProductName), propertyMetadata.PropertyName);
            Assert.False(propertyMetadata.IsPrimaryKey);
            Assert.False(propertyMetadata.IsForeignKey);
            Assert.Equal(typeof(string).FullName, propertyMetadata.TypeName);
            Assert.False(propertyMetadata.IsEnum);
        }

        [Fact]
        public void Enum_Property_Metadata_Is_Correct()
        {
            //Arrange
            var productEntity = TestModel.CategoryProductModel.FindEntityType(typeof(Product));
            var modelMetadata = new ModelMetadata(productEntity, typeof(TestDbContext));

            //Act
            var propertyMetadata = modelMetadata.Properties.FirstOrDefault(p => p.PropertyName == nameof(Product.EnumProperty));

            //Assert
            Assert.Equal(nameof(Product.EnumProperty), propertyMetadata.PropertyName);
            Assert.False(propertyMetadata.IsPrimaryKey);
            Assert.False(propertyMetadata.IsForeignKey);
            Assert.Equal(typeof(EnumType).FullName, propertyMetadata.TypeName);
            Assert.True(propertyMetadata.IsEnum);
            Assert.False(propertyMetadata.IsEnumFlags);
        }

        [Fact]
        public void Enum_Flags_Property_Metadata_Is_Correct()
        {
            //Arrange
            var productEntity = TestModel.CategoryProductModel.FindEntityType(typeof(Product));
            var modelMetadata = new ModelMetadata(productEntity, typeof(TestDbContext));

            //Act
            var propertyMetadata = modelMetadata.Properties.FirstOrDefault(p => p.PropertyName == nameof(Product.EnumFlagsProperty));

            //Assert
            Assert.Equal(nameof(Product.EnumFlagsProperty), propertyMetadata.PropertyName);
            Assert.False(propertyMetadata.IsPrimaryKey);
            Assert.False(propertyMetadata.IsForeignKey);
            Assert.Equal(typeof(EnumFlagsType).FullName, propertyMetadata.TypeName);
            Assert.True(propertyMetadata.IsEnum);
            Assert.True(propertyMetadata.IsEnumFlags);


        }

        [Fact]
        public void Scaffold_Attribute_Is_Reflected_In_Metadata()
        {
            //Arrange
            var productEntity = TestModel.CategoryProductModel.FindEntityType(typeof(Product));
            var modelMetadata = new ModelMetadata(productEntity, typeof(TestDbContext));

            //Act
            var explicitScaffoldProp = modelMetadata.Properties.FirstOrDefault(p => p.PropertyName == nameof(Product.ExplicitScaffoldProperty));
            var scaffoldFalseProp = modelMetadata.Properties.FirstOrDefault(p => p.PropertyName == nameof(Product.ScaffoldFalseProperty));

            //Assert
            Assert.Equal(nameof(Product.ExplicitScaffoldProperty), explicitScaffoldProp.PropertyName);
            Assert.False(explicitScaffoldProp.IsPrimaryKey);
            Assert.False(explicitScaffoldProp.IsForeignKey);
            Assert.Equal(typeof(string).FullName, explicitScaffoldProp.TypeName);
            Assert.True(explicitScaffoldProp.Scaffold);

            Assert.Equal(nameof(Product.ScaffoldFalseProperty), scaffoldFalseProp.PropertyName);
            Assert.False(scaffoldFalseProp.IsPrimaryKey);
            Assert.False(scaffoldFalseProp.IsForeignKey);
            Assert.Equal(typeof(int).FullName, scaffoldFalseProp.TypeName);
            Assert.False(scaffoldFalseProp.Scaffold);
        }

        [Fact(Skip = "Need to investigate/enable the functionality")]
        public void ReadOnly_Attribute_Is_Reflected_In_Metadata()
        {
            //Arrange
            var productEntity = TestModel.CategoryProductModel.FindEntityType(typeof(Product));
            var modelMetadata = new ModelMetadata(productEntity, typeof(TestDbContext));

            //Act
            var propertyMetadata = modelMetadata.Properties.FirstOrDefault(p => p.PropertyName == nameof(Product.ReadOnlyProperty));

            //Assert
            Assert.Equal(nameof(Product.ReadOnlyProperty), propertyMetadata.PropertyName);
            Assert.False(propertyMetadata.IsPrimaryKey);
            Assert.False(propertyMetadata.IsForeignKey);
            Assert.Equal(typeof(string).FullName, propertyMetadata.TypeName);
            Assert.True(propertyMetadata.IsReadOnly);
        }

        [Fact]
        public void PropertyMetadata_From_PropertyInfo()
        {
            //Arrange
            var modelMetadata = new CodeModelMetadata(typeof(Product));

            //Act
            var propertyMetadata = modelMetadata.Properties.FirstOrDefault(p => p.PropertyName == nameof(Product.ProductName));

            //Assert
            Assert.Equal(nameof(Product.ProductName), propertyMetadata.PropertyName);
            Assert.False(propertyMetadata.IsPrimaryKey);
            Assert.False(propertyMetadata.IsEnum);
            Assert.False(propertyMetadata.IsEnumFlags);
            Assert.False(propertyMetadata.IsForeignKey);
            Assert.False(propertyMetadata.IsAutoGenerated);
            Assert.Equal(typeof(string).FullName, propertyMetadata.TypeName);

            //Act
            propertyMetadata = modelMetadata.Properties.FirstOrDefault(p => p.PropertyName == nameof(Product.ProductId));

            //Assert
            Assert.Equal(nameof(Product.ProductId), propertyMetadata.PropertyName);
            Assert.False(propertyMetadata.IsPrimaryKey);
            Assert.False(propertyMetadata.IsEnum);
            Assert.False(propertyMetadata.IsEnumFlags);
            Assert.False(propertyMetadata.IsForeignKey);
            Assert.False(propertyMetadata.IsAutoGenerated);
            Assert.Equal(typeof(int).FullName, propertyMetadata.TypeName);

            //Act
            propertyMetadata = modelMetadata.Properties.FirstOrDefault(p => p.PropertyName == nameof(Product.CategoryId));

            //Assert
            Assert.Equal(nameof(Product.CategoryId), propertyMetadata.PropertyName);
            Assert.False(propertyMetadata.IsPrimaryKey);
            Assert.False(propertyMetadata.IsEnum);
            Assert.False(propertyMetadata.IsEnumFlags);
            Assert.False(propertyMetadata.IsForeignKey);
            Assert.False(propertyMetadata.IsAutoGenerated);
            Assert.Equal(typeof(int).FullName, propertyMetadata.TypeName);
        }
    }
}
