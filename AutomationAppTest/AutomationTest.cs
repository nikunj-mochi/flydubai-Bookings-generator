using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CreatePNR_AutomationApp;

namespace AutomationAppTest
{
    public class AutomationTest
    {
        [Test]
        public void Token_Should_Not_Be_Empty() {
            var expected = "";
            frmCreatePNRs objfrmCreatePNRs = new frmCreatePNRs();
            var actualResult = objfrmCreatePNRs.GetToken();
            Assert.AreNotEqual(expected, actualResult);
        }
    }
}
