﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GatewaysTest.Domain.Common.Language {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class I18n {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal I18n() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GatewaysTest.Domain.Common.Language.I18n", typeof(I18n).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Gateway not foud.
        /// </summary>
        public static string GatewayNotFound {
            get {
                return ResourceManager.GetString("GatewayNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Gateway Removed.
        /// </summary>
        public static string GatewayRemoved {
            get {
                return ResourceManager.GetString("GatewayRemoved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid Ip Address.
        /// </summary>
        public static string InvalidIpAddress {
            get {
                return ResourceManager.GetString("InvalidIpAddress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid Name.
        /// </summary>
        public static string InvalidName {
            get {
                return ResourceManager.GetString("InvalidName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid Serial Number.
        /// </summary>
        public static string InvalidSerialNo {
            get {
                return ResourceManager.GetString("InvalidSerialNo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid Peripheral UID.
        /// </summary>
        public static string InvalidUid {
            get {
                return ResourceManager.GetString("InvalidUid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid Vendor.
        /// </summary>
        public static string InvalidVendor {
            get {
                return ResourceManager.GetString("InvalidVendor", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Only 10 peripherals by each gateway are allowed.
        /// </summary>
        public static string OnlyTenPeripheralsByGateway {
            get {
                return ResourceManager.GetString("OnlyTenPeripheralsByGateway", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Peripheral already exist.
        /// </summary>
        public static string PeripheralAlreadyExits {
            get {
                return ResourceManager.GetString("PeripheralAlreadyExits", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Peripheral not found.
        /// </summary>
        public static string PeripheralNotFound {
            get {
                return ResourceManager.GetString("PeripheralNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Peripheral Removed.
        /// </summary>
        public static string PeripheralRemoved {
            get {
                return ResourceManager.GetString("PeripheralRemoved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Serial Number already exist.
        /// </summary>
        public static string SerialNoAlreadyExist {
            get {
                return ResourceManager.GetString("SerialNoAlreadyExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unknow Error.
        /// </summary>
        public static string UnknowError {
            get {
                return ResourceManager.GetString("UnknowError", resourceCulture);
            }
        }
    }
}
