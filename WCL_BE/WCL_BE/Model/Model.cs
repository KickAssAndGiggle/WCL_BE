using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Identity.Client;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using System.Runtime.Intrinsics.X86;

namespace WCL_BE.Model
{
    public class Model
    {

        public class Fighter
        {
            public long Id;
            public long? GymId;
            public bool Associated;
            public long CountryId;
            public string Country;
            public long CityId;
            public string City;
            public long BackgroundId;
            public string Background;
            public long WeightclassId;
            public string Weightclass;
            public long HeightId;
            public string Height;
            public string FirstName;
            public string Surname;
            public string? Nickname;
            public int Age;
            public int Chin;
            public int Heart;
            public int Strength;
            public int Agility;
            public int Stamina;
            public int Jabs;
            public int Crosses;
            public int Hooks;
            public int Uppercuts;
            public int Legkicks;
            public int Bodykicks;
            public int Headkicks;
            public int Backfists;
            public int Elbows;
            public int Kneestrikes;
            public int Takedowns;
            public int Clinch;
            public int TakedownDefence;
            public int HeadMovement;
            public int Footwork;
            public int Wrestling;
            public int Groundguard;
            public int Chokes;
            public int Armbars;
            public int Leglocks;
        }

        public static object ModelMaker(DataTable tab, Type type)
        {

            ArrayList list = new();
            foreach (DataRow dr in tab.Rows)
            {
                object val = Activator.CreateInstance(type)!;
                foreach (DataColumn dc in tab.Columns)
                {
                    FieldInfo fld = type.GetField(dc.ColumnName)!;
                    if (fld.FieldType == typeof(string) && dr.IsNull(dc))
                    {
                        fld.SetValue(val, "");
                    }
                    else if (fld.FieldType == typeof(DateTime?) && dr.IsNull(dc))
                    {
                        fld.SetValue(val, null);
                    }
                    else if (fld.FieldType == typeof(decimal?) && dr.IsNull(dc))
                    {
                        fld.SetValue(val, null);
                    }
                    else if (fld.FieldType == typeof(int?) && dr.IsNull(dc))
                    {
                        fld.SetValue(val, null);
                    }
                    else if (fld.FieldType == typeof(long?) && dr.IsNull(dc))
                    {
                        fld.SetValue(val, null);
                    }
                    else
                    {
                        fld.SetValue(val, dr[dc.ColumnName]);
                    }
                }
                list.Add(val);
            }

            return list.ToArray(type);

        }

        public static object ModelMaker(DataRow dr, Type type)
        {

            object val = Activator.CreateInstance(type)!;
            foreach (DataColumn dc in dr.Table.Columns)
            {
                FieldInfo fld = type.GetField(dc.ColumnName)!;
                if (fld.FieldType == typeof(string) && dr.IsNull(dc))
                {
                    fld.SetValue(val, "");
                }
                else if (fld.FieldType == typeof(DateTime?) && dr.IsNull(dc))
                {
                    fld.SetValue(val, null);
                }
                else if (fld.FieldType == typeof(decimal?) && dr.IsNull(dc))
                {
                    fld.SetValue(val, null);
                }
                else if (fld.FieldType == typeof(int?) && dr.IsNull(dc))
                {
                    fld.SetValue(val, null);
                }
                else if (fld.FieldType == typeof(long?) && dr.IsNull(dc))
                {
                    fld.SetValue(val, null);
                }
                else
                {
                    fld.SetValue(val, dr[dc.ColumnName]);
                }
            }

            return val;

        }


    }
}
