using CommerceApi;
using CommerceApi.dao;
using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.AspNetCore.Mvc;


namespace CommerceApiTest
{
    public class TransactionLoaderTest
    {
        // expected values are based on the values inside the CustomerATest.csv file

        [Fact]
        public void Test_TransactionReader_LoadList()
        {
            int expectedVal = 74; // rows
            int actualVal = CommerceApi.TransactionReader.ImportTransactions("test_input/CustomerATest.csv").Count;
            Assert.Equal(expectedVal, actualVal);
        }

        [Fact]
        public void Test_TransactionReader_BlankCell()
        {
            string expectedVal = "";
            Transaction actualVal = CommerceApi.TransactionReader.ImportTransactions("test_input/CustomerATest_BlankCell.csv")[0];
            Assert.Equal(expectedVal, actualVal.processDate);
        }

        [Fact]
        public void Test_TransactionReader_DollarAmount()
        {
            string expectedVal = "$800.00";
            Transaction actualVal = CommerceApi.TransactionReader.ImportTransactions("test_input/CustomerATest.csv")[2];
            Assert.Equal(expectedVal, actualVal.amount);
        }

        [Fact]
        public void Test_TransactionReader_LastCell()
        {
            string expectedVal = "Starbucks";
            Transaction actualVal = CommerceApi.TransactionReader.ImportTransactions("test_input/CustomerATest.csv")[1];
            Assert.Equal(expectedVal, actualVal.description);
        }
    }
 }










