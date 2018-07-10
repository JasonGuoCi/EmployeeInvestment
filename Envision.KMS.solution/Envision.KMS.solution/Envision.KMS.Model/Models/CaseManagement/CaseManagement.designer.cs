﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Envision.KMS.Model.Models.CaseManagement
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Envision.KMS.DB")]
	public partial class CaseManagementDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertCaseManagementTask(CaseManagementTask instance);
    partial void UpdateCaseManagementTask(CaseManagementTask instance);
    partial void DeleteCaseManagementTask(CaseManagementTask instance);
    #endregion
		
		public CaseManagementDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CaseManagementDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CaseManagementDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CaseManagementDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<CaseManagementTask> CaseManagementTasks
		{
			get
			{
				return this.GetTable<CaseManagementTask>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.CaseManagementTask")]
	public partial class CaseManagementTask : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _CaseId;
		
		private string _CaseTitle;
		
		private string _AssignedToAccount;
		
		private string _AssignedToDisplayName;
		
		private string _Status;
		
		private string _Comments;
		
		private string _Result;
		
		private System.DateTime _Created;
		
		private System.DateTime _Modified;
		
		private string _CaseType;
		
		private string _WebID;
		
		private string _CreatedBy;
		
		private string _CreatedByAccount;
		
		private string _AssignedToEmail;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnCaseIdChanging(string value);
    partial void OnCaseIdChanged();
    partial void OnCaseTitleChanging(string value);
    partial void OnCaseTitleChanged();
    partial void OnAssignedToAccountChanging(string value);
    partial void OnAssignedToAccountChanged();
    partial void OnAssignedToDisplayNameChanging(string value);
    partial void OnAssignedToDisplayNameChanged();
    partial void OnStatusChanging(string value);
    partial void OnStatusChanged();
    partial void OnCommentsChanging(string value);
    partial void OnCommentsChanged();
    partial void OnResultChanging(string value);
    partial void OnResultChanged();
    partial void OnCreatedChanging(System.DateTime value);
    partial void OnCreatedChanged();
    partial void OnModifiedChanging(System.DateTime value);
    partial void OnModifiedChanged();
    partial void OnCaseTypeChanging(string value);
    partial void OnCaseTypeChanged();
    partial void OnWebIDChanging(string value);
    partial void OnWebIDChanged();
    partial void OnCreatedByChanging(string value);
    partial void OnCreatedByChanged();
    partial void OnCreatedByAccountChanging(string value);
    partial void OnCreatedByAccountChanged();
    partial void OnAssignedToEmailChanging(string value);
    partial void OnAssignedToEmailChanged();
    #endregion
		
		public CaseManagementTask()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CaseId", DbType="NVarChar(250)")]
		public string CaseId
		{
			get
			{
				return this._CaseId;
			}
			set
			{
				if ((this._CaseId != value))
				{
					this.OnCaseIdChanging(value);
					this.SendPropertyChanging();
					this._CaseId = value;
					this.SendPropertyChanged("CaseId");
					this.OnCaseIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CaseTitle", DbType="NVarChar(500)")]
		public string CaseTitle
		{
			get
			{
				return this._CaseTitle;
			}
			set
			{
				if ((this._CaseTitle != value))
				{
					this.OnCaseTitleChanging(value);
					this.SendPropertyChanging();
					this._CaseTitle = value;
					this.SendPropertyChanged("CaseTitle");
					this.OnCaseTitleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AssignedToAccount", DbType="NVarChar(250)")]
		public string AssignedToAccount
		{
			get
			{
				return this._AssignedToAccount;
			}
			set
			{
				if ((this._AssignedToAccount != value))
				{
					this.OnAssignedToAccountChanging(value);
					this.SendPropertyChanging();
					this._AssignedToAccount = value;
					this.SendPropertyChanged("AssignedToAccount");
					this.OnAssignedToAccountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AssignedToDisplayName", DbType="NVarChar(250)")]
		public string AssignedToDisplayName
		{
			get
			{
				return this._AssignedToDisplayName;
			}
			set
			{
				if ((this._AssignedToDisplayName != value))
				{
					this.OnAssignedToDisplayNameChanging(value);
					this.SendPropertyChanging();
					this._AssignedToDisplayName = value;
					this.SendPropertyChanged("AssignedToDisplayName");
					this.OnAssignedToDisplayNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Status", DbType="NVarChar(50)")]
		public string Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				if ((this._Status != value))
				{
					this.OnStatusChanging(value);
					this.SendPropertyChanging();
					this._Status = value;
					this.SendPropertyChanged("Status");
					this.OnStatusChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Comments", DbType="NVarChar(MAX)")]
		public string Comments
		{
			get
			{
				return this._Comments;
			}
			set
			{
				if ((this._Comments != value))
				{
					this.OnCommentsChanging(value);
					this.SendPropertyChanging();
					this._Comments = value;
					this.SendPropertyChanged("Comments");
					this.OnCommentsChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Result", DbType="NVarChar(500)")]
		public string Result
		{
			get
			{
				return this._Result;
			}
			set
			{
				if ((this._Result != value))
				{
					this.OnResultChanging(value);
					this.SendPropertyChanging();
					this._Result = value;
					this.SendPropertyChanged("Result");
					this.OnResultChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Created", DbType="DateTime NOT NULL")]
		public System.DateTime Created
		{
			get
			{
				return this._Created;
			}
			set
			{
				if ((this._Created != value))
				{
					this.OnCreatedChanging(value);
					this.SendPropertyChanging();
					this._Created = value;
					this.SendPropertyChanged("Created");
					this.OnCreatedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Modified", DbType="DateTime NOT NULL")]
		public System.DateTime Modified
		{
			get
			{
				return this._Modified;
			}
			set
			{
				if ((this._Modified != value))
				{
					this.OnModifiedChanging(value);
					this.SendPropertyChanging();
					this._Modified = value;
					this.SendPropertyChanged("Modified");
					this.OnModifiedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CaseType", DbType="NVarChar(500)")]
		public string CaseType
		{
			get
			{
				return this._CaseType;
			}
			set
			{
				if ((this._CaseType != value))
				{
					this.OnCaseTypeChanging(value);
					this.SendPropertyChanging();
					this._CaseType = value;
					this.SendPropertyChanged("CaseType");
					this.OnCaseTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WebID", DbType="NVarChar(250)")]
		public string WebID
		{
			get
			{
				return this._WebID;
			}
			set
			{
				if ((this._WebID != value))
				{
					this.OnWebIDChanging(value);
					this.SendPropertyChanging();
					this._WebID = value;
					this.SendPropertyChanged("WebID");
					this.OnWebIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreatedBy", DbType="NVarChar(250)")]
		public string CreatedBy
		{
			get
			{
				return this._CreatedBy;
			}
			set
			{
				if ((this._CreatedBy != value))
				{
					this.OnCreatedByChanging(value);
					this.SendPropertyChanging();
					this._CreatedBy = value;
					this.SendPropertyChanged("CreatedBy");
					this.OnCreatedByChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreatedByAccount", DbType="NVarChar(250)")]
		public string CreatedByAccount
		{
			get
			{
				return this._CreatedByAccount;
			}
			set
			{
				if ((this._CreatedByAccount != value))
				{
					this.OnCreatedByAccountChanging(value);
					this.SendPropertyChanging();
					this._CreatedByAccount = value;
					this.SendPropertyChanged("CreatedByAccount");
					this.OnCreatedByAccountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AssignedToEmail", DbType="NVarChar(250)")]
		public string AssignedToEmail
		{
			get
			{
				return this._AssignedToEmail;
			}
			set
			{
				if ((this._AssignedToEmail != value))
				{
					this.OnAssignedToEmailChanging(value);
					this.SendPropertyChanging();
					this._AssignedToEmail = value;
					this.SendPropertyChanged("AssignedToEmail");
					this.OnAssignedToEmailChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
