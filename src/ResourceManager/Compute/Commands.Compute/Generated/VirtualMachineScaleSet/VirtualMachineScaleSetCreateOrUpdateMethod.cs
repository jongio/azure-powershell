// 
// Copyright (c) Microsoft and contributors.  All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// 
// See the License for the specific language governing permissions and
// limitations under the License.
// 

// Warning: This code was generated by a tool.
// 
// Changes to this file may cause incorrect behavior and will be lost if the
// code is regenerated.

using Microsoft.Azure.Commands.Common.Strategies;
using Microsoft.Azure.Commands.Common.Strategies.Compute;
using Microsoft.Azure.Commands.Common.Strategies.Network;
using Microsoft.Azure.Commands.Common.Strategies.ResourceManager;
using Microsoft.Azure.Commands.Compute.Automation.Models;
using Microsoft.Azure.Commands.Compute.Strategies;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.Compute;
using Microsoft.Azure.Management.Compute.Models;
using Microsoft.WindowsAzure.Commands.Utilities.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Azure.Commands.Compute.Automation
{
    public partial class InvokeAzureComputeMethodCmdlet : ComputeAutomationBaseCmdlet
    {
        protected object CreateVirtualMachineScaleSetCreateOrUpdateDynamicParameters()
        {
            dynamicParameters = new RuntimeDefinedParameterDictionary();
            var pResourceGroupName = new RuntimeDefinedParameter();
            pResourceGroupName.Name = "ResourceGroupName";
            pResourceGroupName.ParameterType = typeof(string);
            pResourceGroupName.Attributes.Add(new ParameterAttribute
            {
                ParameterSetName = "InvokeByDynamicParameters",
                Position = 1,
                Mandatory = true
            });
            pResourceGroupName.Attributes.Add(new AllowNullAttribute());
            dynamicParameters.Add("ResourceGroupName", pResourceGroupName);

            var pVMScaleSetName = new RuntimeDefinedParameter();
            pVMScaleSetName.Name = "VMScaleSetName";
            pVMScaleSetName.ParameterType = typeof(string);
            pVMScaleSetName.Attributes.Add(new ParameterAttribute
            {
                ParameterSetName = "InvokeByDynamicParameters",
                Position = 2,
                Mandatory = true
            });
            pVMScaleSetName.Attributes.Add(new AllowNullAttribute());
            dynamicParameters.Add("VMScaleSetName", pVMScaleSetName);

            var pParameters = new RuntimeDefinedParameter();
            pParameters.Name = "VirtualMachineScaleSet";
            pParameters.ParameterType = typeof(VirtualMachineScaleSet);
            pParameters.Attributes.Add(new ParameterAttribute
            {
                ParameterSetName = "InvokeByDynamicParameters",
                Position = 3,
                Mandatory = true
            });
            pParameters.Attributes.Add(new AllowNullAttribute());
            dynamicParameters.Add("VirtualMachineScaleSet", pParameters);

            var pArgumentList = new RuntimeDefinedParameter();
            pArgumentList.Name = "ArgumentList";
            pArgumentList.ParameterType = typeof(object[]);
            pArgumentList.Attributes.Add(new ParameterAttribute
            {
                ParameterSetName = "InvokeByStaticParameters",
                Position = 4,
                Mandatory = true
            });
            pArgumentList.Attributes.Add(new AllowNullAttribute());
            dynamicParameters.Add("ArgumentList", pArgumentList);

            return dynamicParameters;
        }

        protected void ExecuteVirtualMachineScaleSetCreateOrUpdateMethod(object[] invokeMethodInputParameters)
        {
            string resourceGroupName = (string)ParseParameter(invokeMethodInputParameters[0]);
            string vmScaleSetName = (string)ParseParameter(invokeMethodInputParameters[1]);
            VirtualMachineScaleSet parameters = (VirtualMachineScaleSet)ParseParameter(invokeMethodInputParameters[2]);

            var result = VirtualMachineScaleSetsClient.CreateOrUpdate(resourceGroupName, vmScaleSetName, parameters);
            WriteObject(result);
        }
    }

    public partial class NewAzureComputeArgumentListCmdlet : ComputeAutomationBaseCmdlet
    {
        protected PSArgument[] CreateVirtualMachineScaleSetCreateOrUpdateParameters()
        {
            string resourceGroupName = string.Empty;
            string vmScaleSetName = string.Empty;
            VirtualMachineScaleSet parameters = new VirtualMachineScaleSet();

            return ConvertFromObjectsToArguments(
                 new string[] { "ResourceGroupName", "VMScaleSetName", "Parameters" },
                 new object[] { resourceGroupName, vmScaleSetName, parameters });
        }
    }

    [Cmdlet(VerbsCommon.New, "AzureRmVmss", DefaultParameterSetName = "DefaultParameter", SupportsShouldProcess = true)]
    [OutputType(typeof(PSVirtualMachineScaleSet))]
    public partial class NewAzureRmVmss : ComputeAutomationBaseCmdlet
    {
        public const string SimpleParameterSet = "SimpleParameterSet";

        public override void ExecuteCmdlet()
        {
            switch (ParameterSetName)
            {
                case SimpleParameterSet:
                    this.StartAndWait(SimpleParameterSetExecuteCmdlet);
                    break;
                default:
                    ExecuteClientAction(() =>
                    {
                        if (ShouldProcess(this.VMScaleSetName, VerbsCommon.New))
                        {
                            string resourceGroupName = this.ResourceGroupName;
                            string vmScaleSetName = this.VMScaleSetName;
                            VirtualMachineScaleSet parameters = new VirtualMachineScaleSet();
                            ComputeAutomationAutoMapperProfile.Mapper.Map<PSVirtualMachineScaleSet, VirtualMachineScaleSet>(this.VirtualMachineScaleSet, parameters);

                            var result = VirtualMachineScaleSetsClient.CreateOrUpdate(resourceGroupName, vmScaleSetName, parameters);
                            var psObject = new PSVirtualMachineScaleSet();
                            ComputeAutomationAutoMapperProfile.Mapper.Map<VirtualMachineScaleSet, PSVirtualMachineScaleSet>(result, psObject);
                            WriteObject(psObject);
                        }
                    });
                    break;
            }
        }

        async Task SimpleParameterSetExecuteCmdlet(IAsyncCmdlet asyncCmdlet)
        {
            ResourceGroupName = ResourceGroupName ?? VMScaleSetName;
            VirtualNetworkName = VirtualNetworkName ?? VMScaleSetName;
            SubnetName = SubnetName ?? VMScaleSetName;
            PublicIpAddressName = PublicIpAddressName ?? VMScaleSetName;
            DomainNameLabel = DomainNameLabel ?? (VMScaleSetName + ResourceGroupName).ToLower();
            SecurityGroupName = SecurityGroupName ?? VMScaleSetName;
            LoadBalancerName = LoadBalancerName ?? VMScaleSetName;
            FrontendPoolName = FrontendPoolName ?? VMScaleSetName;
            BackendPoolName = BackendPoolName ?? VMScaleSetName;

            // get image
            bool isWindows;
            Commands.Common.Strategies.Compute.Image image;
            if (ImageName.Contains(':'))
            {
                var imageArray = ImageName.Split(':');
                if (imageArray.Length != 4)
                {
                    throw new Exception("Invalid ImageName");
                }
                image = new Commands.Common.Strategies.Compute.Image
                {
                    publisher = imageArray[0],
                    offer = imageArray[1],
                    sku = imageArray[2],
                    version = imageArray[3],
                };
                isWindows = image.publisher.ToLower() == "MicrosoftWindowsServer".ToLower();
            }
            else
            {
                // get image
                var osTypeAndImage = Images
                    .Instance
                    .SelectMany(osAndMap => osAndMap
                        .Value
                        .Where(nameAndImage => nameAndImage.Key.ToLower() == ImageName.ToLower())
                        .Select(nameAndImage => new
                        {
                            OsType = osAndMap.Key,
                            Image = nameAndImage.Value
                        }))
                    .FirstOrDefault();
                image = osTypeAndImage.Image;
                isWindows = osTypeAndImage.OsType == "Windows";
            }

            BackendPort = BackendPort ?? (isWindows ? new[] { 3389, 5985 } : new[] { 22 });
            
            var resourceGroup = ResourceGroupStrategy.CreateResourceGroupConfig(ResourceGroupName);
            
            var publicIpAddress = resourceGroup.CreatePublicIPAddressConfig(
                name: PublicIpAddressName,
                domainNameLabel: DomainNameLabel,
                allocationMethod: AllocationMethod);
            
            var virtualNetwork = resourceGroup.CreateVirtualNetworkConfig(
                name: VirtualNetworkName, addressPrefix: VnetAddressPrefix);

            var subnet = virtualNetwork.CreateSubnet(SubnetName, SubnetAddressPrefix);

            var loadBalancer = resourceGroup.CreateLoadBalancerConfig(
                name: LoadBalancerName,
                froontendPoolName: FrontendPoolName,
                backendPoolName: BackendPoolName,
                zones: Zone,
                publicIPAddress: publicIpAddress,
                subnet: subnet);

            var frontendIpConfiguration = loadBalancer.CreateFrontendIPConfiguration(
                name: FrontendPoolName,
                zones: Zone,
                publicIPAddress: publicIpAddress,
                subnet: subnet);

            var backendAddressPool = loadBalancer.CreateBackendAddressPool(
                name: BackendPoolName);

            var virtualMachineScaleSet = resourceGroup.CreateVirtualMachineScaleSetConfig(
                name: VMScaleSetName,
                subnet: subnet,
                frontendIpConfigurations: new[] { frontendIpConfiguration },
                backendAdressPool: backendAddressPool,
                isWindows: isWindows,
                adminUsername: Credential.UserName,
                adminPassword: new NetworkCredential(string.Empty, Credential.Password).Password,
                image: image,
                vmSize: VmSize,
                instanceCount: InstanceCount,
                upgradeMode: (MyInvocation.BoundParameters.ContainsKey("UpgradePolicyMode") == true ) ? UpgradePolicyMode : (UpgradeMode?) null);

            var client = new Client(DefaultProfile.DefaultContext);

            // get current Azure state
            var current = await virtualMachineScaleSet.GetStateAsync(client, new CancellationToken());

            if (Location == null)
            {
                Location = current.GetLocation(virtualMachineScaleSet);
                if (Location == null)
                {
                    Location = "eastus";
                }
            }

            var target = virtualMachineScaleSet.GetTargetState(current, client.SubscriptionId, Location);

            var newState = await virtualMachineScaleSet
               .UpdateStateAsync(
                   client,
                   target,
                   new CancellationToken(),
                   new ShouldProcess(asyncCmdlet),
                    asyncCmdlet.ReportTaskProgress);

            var result = newState.Get(virtualMachineScaleSet);
            if(result == null)
            {
                result = current.Get(virtualMachineScaleSet);
            }

            if (result != null)
            {
                var psObject = new PSVirtualMachineScaleSet();
                ComputeAutomationAutoMapperProfile.Mapper.Map<VirtualMachineScaleSet, PSVirtualMachineScaleSet>(result, psObject);
                asyncCmdlet.WriteObject(psObject);
            }
        }

        [Parameter(Mandatory = false, HelpMessage = "Run cmdlet in the background")]
        public SwitchParameter AsJob { get; set; }

        [Parameter(
            ParameterSetName = "DefaultParameter",
            Position = 1,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            ValueFromPipeline = false)]
        [AllowNull]
        [ResourceManager.Common.ArgumentCompleters.ResourceGroupCompleter()]
        [Parameter(
            ParameterSetName = SimpleParameterSet,
            Mandatory = false)]
        public string ResourceGroupName { get; set; }

        [Parameter(
            ParameterSetName = "DefaultParameter",
            Position = 2,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            ValueFromPipeline = false)]
        [Alias("Name")]
        [AllowNull]
        [Parameter(
            ParameterSetName = SimpleParameterSet,
            Mandatory = true)]
        public string VMScaleSetName { get; set; }

        [Parameter(
            ParameterSetName = "DefaultParameter",
            Position = 3,
            Mandatory = true,
            ValueFromPipelineByPropertyName = false,
            ValueFromPipeline = true)]
        [AllowNull]
        public PSVirtualMachineScaleSet VirtualMachineScaleSet { get; set; }

        // SimpleParameterSet

        [Parameter(ParameterSetName = SimpleParameterSet, Mandatory = false)]
        [PSArgumentCompleter(
            "CentOS",
            "CoreOS",
            "Debian",
            "openSUSE-Leap",
            "RHEL",
            "SLES",
            "UbuntuLTS",
            "Win2016Datacenter",
            "Win2012R2Datacenter",
            "Win2012Datacenter",
            "Win2008R2SP1")]
        public string ImageName { get; set; } = "Win2016Datacenter";

        [Parameter(ParameterSetName = SimpleParameterSet, Mandatory = true)]
        public PSCredential Credential { get; set; }

        [Parameter(ParameterSetName = SimpleParameterSet, Mandatory = false)]
        public int InstanceCount { get; set; } = 2;

        [Parameter(ParameterSetName = SimpleParameterSet, Mandatory = false)]
        public string VirtualNetworkName { get; set; }

        [Parameter(ParameterSetName = SimpleParameterSet, Mandatory = false)]
        public string SubnetName { get; set; }

        [Parameter(ParameterSetName = SimpleParameterSet, Mandatory = false)]
        public string PublicIpAddressName { get; set; }
        
        [Parameter(ParameterSetName = SimpleParameterSet, Mandatory = false)]
        public string DomainNameLabel { get; set; }

        [Parameter(ParameterSetName = SimpleParameterSet, Mandatory = false)]
        public string SecurityGroupName { get; set; }

        [Parameter(ParameterSetName = SimpleParameterSet, Mandatory = false)]
        public string LoadBalancerName { get; set; }

        [Parameter(ParameterSetName = SimpleParameterSet, Mandatory = false)]
        public int[] BackendPort { get; set; }

        [Parameter(ParameterSetName = SimpleParameterSet, Mandatory = false)]
        [LocationCompleter("Microsoft.Compute/virtualMachines")]
        public string Location { get; set; }

        // this corresponds to VmSku in the Azure CLI
        [Parameter(ParameterSetName = SimpleParameterSet, Mandatory = false)]
        public string VmSize { get; set; } = "Standard_DS1_v2";

        [Parameter(ParameterSetName = SimpleParameterSet, Mandatory = false)]
        public UpgradeMode UpgradePolicyMode { get; set; }

        [Parameter(ParameterSetName = SimpleParameterSet, Mandatory = false)]
        [ValidateSet("Static", "Dynamic")]
        public string AllocationMethod { get; set; } = "Static";
        
        [Parameter(ParameterSetName = SimpleParameterSet, Mandatory = false)]
        public string VnetAddressPrefix { get; set; } = "192.168.0.0/16";
        
        [Parameter(ParameterSetName = SimpleParameterSet, Mandatory = false)]
        public string SubnetAddressPrefix { get; set; } = "192.168.1.0/24";

        [Parameter(ParameterSetName = SimpleParameterSet, Mandatory = false)]
        public string FrontendPoolName { get; set; }

        [Parameter(ParameterSetName = SimpleParameterSet, Mandatory = false)]
        public string BackendPoolName { get; set; }

        [Parameter(
            ParameterSetName = SimpleParameterSet,
            Mandatory = false,
            HelpMessage = "A list of availability zones denoting the IP allocated for the resource needs to come from.",
            ValueFromPipelineByPropertyName = true)]
        public List<string> Zone { get; set; }
    }
}
