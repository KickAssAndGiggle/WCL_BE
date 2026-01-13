using System.Collections;
using System.Data;
using System.Reflection;

namespace WCL_BE.Model
{
    public class Model
    {




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
