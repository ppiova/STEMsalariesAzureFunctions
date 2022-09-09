﻿﻿// This file was auto-generated by ML.NET Model Builder. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML;

namespace STEMsalariesModelBuilder
{
    public partial class STEMSalary
    {
        public static ITransformer RetrainPipeline(MLContext context, IDataView trainData)
        {
            var pipeline = BuildPipeline(context);
            var model = pipeline.Fit(trainData);

            return model;
        }

        /// <summary>
        /// build the pipeline that is used from model builder. Use this function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.Categorical.OneHotEncoding(new []{new InputOutputColumnPair(@"Title", @"Title"),new InputOutputColumnPair(@"Gender", @"Gender"),new InputOutputColumnPair(@"Race", @"Race"),new InputOutputColumnPair(@"Education", @"Education")})      
                                    .Append(mlContext.Transforms.ReplaceMissingValues(new []{new InputOutputColumnPair(@"YearsExperience", @"YearsExperience"),new InputOutputColumnPair(@"YearsCompany", @"YearsCompany")}))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName:@"Company",outputColumnName:@"Company"))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName:@"Location",outputColumnName:@"Location"))      
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new []{@"Title",@"Gender",@"Race",@"Education",@"YearsExperience",@"YearsCompany",@"Company",@"Location"}))      
                                    .Append(mlContext.Transforms.NormalizeMinMax(@"Features", @"Features"))      
                                    .Append(mlContext.Regression.Trainers.LbfgsPoissonRegression(new LbfgsPoissonRegressionTrainer.Options(){L1Regularization=0.03125F,L2Regularization=0.03125F,LabelColumnName=@"AnualCompensation",FeatureColumnName=@"Features"}));

            return pipeline;
        }
    }
}
