using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace UIRenderers
{
    public partial class ListaIventarios : DevExpress.XtraEditors.XtraForm
    {
        private Dictionary<string, object[]> dictionaryOfRows = new Dictionary<string, object[]>();
        bool dataHasChanged;
        public ListaIventarios(Form parentForm)
        {
            InitializeComponent();
            InitConexionDB();
            RefreshGrid();
            if (parentForm != null)
            {
                Left = parentForm.Left + (parentForm.Width - Width) / 2;
                Top = parentForm.Top + (parentForm.Height - Height) / 2;
            }
        }

        private void InitConexionDB()
        {
            CommonUtils.ConexionBD.StringDeConexion = CommonUtils.ConexionBD.GetConnectionString();//GetConnectionString();
        }
              
        private void RefreshGrid()
        {
            string sqlQuery = "SELECT InsumoId,Nombre,Presentacion,Marca,Unidad,CantidadInsumoEnIventario FROM Insumo";
            gridControlListaIventarios.DataSource = null;
            gridControlListaIventarios.DataSource = CommonUtils.ConexionBD.EjecutarConsulta(sqlQuery);
            gridViewListaIventarios.Columns[0].FieldName = "InsumoId";
            gridViewListaIventarios.Columns[1].FieldName = "Nombre";
            gridViewListaIventarios.Columns[2].FieldName = "Presentacion";
            gridViewListaIventarios.Columns[3].FieldName = "Marca";
            gridViewListaIventarios.Columns[4].FieldName = "Unidad";
            gridViewListaIventarios.Columns[5].FieldName = "CantidadInsumoEnIventario";
            
        }

        private void barButtonUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barButtonDown.Enabled = true;

            if (gridViewListaIventarios.FocusedRowHandle > 1)
                gridViewListaIventarios.MovePrev();
            else
            {
                gridViewListaIventarios.MovePrev();
                barButtonUp.Enabled = false;
            }
        }

        private void barButtonDown_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barButtonUp.Enabled = true;

            if (gridViewListaIventarios.FocusedRowHandle + 1 < gridViewListaIventarios.RowCount - 1)
                gridViewListaIventarios.MoveNext();
            else
            {
                gridViewListaIventarios.MoveNext();
                barButtonDown.Enabled = false;
            }
        }

        private void manageUpdateFeature(object[] dr)
        {
            string sqlQuery = "UPDATE Insumo set CantidadInsumoEnIventario="+dr[1].ToString()+" WHERE InsumoId="+dr[0].ToString();
            CommonUtils.ConexionBD.Actualizar(sqlQuery);    
        }

        private void gridViewListaIventarios_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row;
            row = gridViewListaIventarios.GetDataRow(e.RowHandle);

            if (row == null || row[0].ToString() == string.Empty)
            {
                return;
            }
            if ((e.Value.ToString().Trim() == string.Empty))
            {
                ribbonControl.Enabled = false;
                return;
            }
            else
            {
                ribbonControl.Enabled = true;
            }
            dataHasChanged = true;
            object[] array = new object[2];
            array[0] = row[0].ToString();//id
            array[1] = row[5].ToString();//cantidadInsumoEnIventario         

            if (gridViewListaIventarios.FocusedColumn.FieldName == "CantidadInsumoEnIventario")
            {
                array[1] = e.Value.ToString();
            }
          
            if (dictionaryOfRows.ContainsKey(row[0].ToString()))
            {
                dictionaryOfRows.Remove(row[0].ToString());
            }
            dictionaryOfRows.Add
                (row[0].ToString(), array);
        }

        private void barButtonEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (object[] obj in dictionaryOfRows.Values)
            {
                manageUpdateFeature(obj);
            }
            RefreshGrid();
            dictionaryOfRows.Clear();
            dataHasChanged = false;
        }

        private void gridControlListaIventarios_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DataRow r = gridViewListaIventarios.GetDataRow(gridViewListaIventarios.FocusedRowHandle);
                if (dictionaryOfRows.ContainsKey(r[0].ToString()))
                {
                    dictionaryOfRows.Remove(r[0].ToString());
                    object[] array = new object[2];
                    array[0] = r[0].ToString();
                    array[1] = r[5].ToString();                   
                    dictionaryOfRows.Add(r[0].ToString(), array);
                }
                ribbonControl.Enabled = true;
            }
        }
      
        private void gridViewListaIventarios_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            DataRow rowSelected = gridViewListaIventarios.GetDataRow(gridViewListaIventarios.FocusedRowHandle);
            if (rowSelected == null)
            {
                return;
            }
            if (gridViewListaIventarios.FocusedColumn.FieldName == "CantidadInsumoEnIventario")
            {
                if (e.Value == null || e.Value.ToString().Trim().Length == 0)
                {
                    e.Valid = false;
                    e.ErrorText = "Columna Cantidad en Almacen no puede estar vacia";
                }
            }
        }

      

        private void ListaIventarios_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dataHasChanged)
            {
                if (MessageBox.Show(this, "Existen cambios en la tabla, desea salir sin guardar los cambios? ", "Inventario", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    e.Cancel=true;
                }
            }
        }        
    }
}