using LOTK.Model;
// <copyright file="UserActionHandlerTest.cs">Copyright ©  2016</copyright>

using System;
using LOTK.Controller;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LOTK.Controller.Tests
{
    [TestClass]
    [PexClass(typeof(UserActionHandler))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class UserActionHandlerTest
    {

        [PexMethod]
        [PexAllowedException(typeof(NullReferenceException))]
        public UserAction clickOK([PexAssumeUnderTest]UserActionHandler target, int playerID)
        {
            UserAction result = target.clickOK(playerID);
            return result;
            // TODO: add assertions to method UserActionHandlerTest.clickOK(UserActionHandler, Int32)
        }
    }
}
