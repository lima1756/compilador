using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
namespace IDE
{
    

    public partial class Form1 : Form
    {   //tama y tamd no se usan en el codigo, su uso fue reemplazado por a y d
        int tama=0, tamd=0;
        //estas variables (a y d) se encargan de verificar si hubo algun cambio, a es el antes y d es el despues :3
        string a = "", d = "";

        // con esto logro que cada que habra un form, no tenga un nombre de archivo, ni ruta
        string nombrearchivo = "";
        string rutaynom = "";
        public Form1()
        {
            InitializeComponent();
            this.Text = "Sin nombre";
          
        }






        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            {
               
                SaveFileDialog guardarcomo = new SaveFileDialog();
                guardarcomo.AddExtension = true;
              
                //la extension por default si no escribe alguna
                guardarcomo.DefaultExt = "txt";
                guardarcomo.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
                
                //verifica si al abrir el dialogo de guardar tuvo un resultado y si ingreso un nombre 
                if (guardarcomo.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
                   guardarcomo.FileName.Length > 0)
                {
                    // nombrearchivo, y rutaynom adquieren el nombre y ruta que se eligio en el cuadro de dialogo guardar
                   nombrearchivo = System.IO.Path.GetFileName(guardarcomo.FileName);
                    rutaynom = guardarcomo.FileName;
                    this.Text = nombrearchivo;
                    //aqui se guarda el archivo con el nombre del archivo y su ruta
                    richTextBox1.SaveFile(guardarcomo.FileName, RichTextBoxStreamType.PlainText);
                    tama = richTextBox1.Text.Length;
                    a = richTextBox1.Text;
                    // al momento de guardar a=al string que habia en el richtextbox 
                }

            }

                       
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
           /* guardarComoToolStripMenuItem.Enabled = true;
            guardarToolStripMenuItem.Enabled = true;
            edicionToolStripMenuItem.Enabled = true;
            richTextBox1.Show();*/
            OpenFileDialog abrir = new OpenFileDialog();
            

            tamd = richTextBox1.Text.Length;
            // d es igual al string que hay actualmente en el richtextbox
            d = richTextBox1.Text;
            //verifica si hubo cambios desde la ultima vez que se guardo
            if (!(a == d))
            {

                //para un archivo que sea nuevo
                if (nombrearchivo == "")
                {
                    if (MessageBox.Show("¿Quieres guardar cambios a este archivo sin nombre?", "IDE",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    == DialogResult.Yes)
                    {
                        // se realiza el proceso de guardar como
                    

                        SaveFileDialog guardarcomo = new SaveFileDialog();
                        guardarcomo.AddExtension = true;

                        guardarcomo.DefaultExt = "txt";
                        guardarcomo.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";

                        if (guardarcomo.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
                           guardarcomo.FileName.Length > 0)
                        {
                            nombrearchivo = System.IO.Path.GetFileName(guardarcomo.FileName);
                            rutaynom = guardarcomo.FileName;
                            this.Text = nombrearchivo;
                            richTextBox1.SaveFile(guardarcomo.FileName, RichTextBoxStreamType.PlainText);
                            tama = richTextBox1.Text.Length;
                            a = richTextBox1.Text;

                        }
                     
                    }

                    else {
                        // no quiso guardar 
                        MessageBox.Show("ok");

                        if (abrir.ShowDialog() == DialogResult.OK)
                        {
                            //abre el cuadro dialogo de abrir
                        //rutaynom, nombrearchivo adquieren el nombre y ruta del ahora seleccionado en el cuadro dialogo de abrir
                            nombrearchivo = System.IO.Path.GetFileName(abrir.FileName);

                            rutaynom = abrir.FileName;
                            //lee y adquiere lo que habia en el archivo que se abrio
                            richTextBox1.Text = File.ReadAllText(abrir.FileName);
                            this.Text = nombrearchivo;
                            //tam a ahora adquire el string del archivo abierto
                            tama = richTextBox1.Text.Length;
                            a = richTextBox1.Text;
                        }
                        else {
                            MessageBox.Show("pusiste cancelar");

                        }

                    }
                    }

                // no es un archivo nuevo, pero se ha realizado cambios desde la ultima vez que guardo
                else {

                    if (MessageBox.Show("¿Quieres guardar cambios a " + rutaynom + "?", "IDE",
          MessageBoxButtons.YesNo, MessageBoxIcon.Question)
          == DialogResult.Yes)
                    {
                        
                        // abrir.FileName adquiere la rutanom del ultimo guardado
                        abrir.FileName = rutaynom;
                        //guarda el archivo en esa ruta y nombre
                        richTextBox1.SaveFile(abrir.FileName, RichTextBoxStreamType.PlainText);

                        if (abrir.ShowDialog() == DialogResult.OK)
                        {
                         

                            nombrearchivo = System.IO.Path.GetFileName(abrir.FileName);
                            rutaynom = abrir.FileName;
                            richTextBox1.Text = File.ReadAllText(abrir.FileName);
                            this.Text = nombrearchivo;
                            tama = richTextBox1.Text.Length;
                            a = richTextBox1.Text;
                        }
                    }


                    else {
                        MessageBox.Show("ok");
                        if (abrir.ShowDialog() == DialogResult.OK)
                        {

                            nombrearchivo = System.IO.Path.GetFileName(abrir.FileName);
                            rutaynom = abrir.FileName;
                            richTextBox1.Text = File.ReadAllText(abrir.FileName);
                            this.Text = nombrearchivo;
                            tama = richTextBox1.Text.Length;
                            a = richTextBox1.Text;
                        }

                    }
                }

                }
                /*sin cambios en texto*/
                else {
                    if (abrir.ShowDialog() == DialogResult.OK)
                    {
                    salirToolStripMenuItem.Enabled = true;
                    guardarComoToolStripMenuItem.Enabled = true;
                    guardarToolStripMenuItem.Enabled = true;
                    edicionToolStripMenuItem.Enabled = true;
                    richTextBox1.Show();



                    nombrearchivo = System.IO.Path.GetFileName(abrir.FileName);
                        rutaynom = abrir.FileName;
                        richTextBox1.Text = File.ReadAllText(abrir.FileName);
                        this.Text = nombrearchivo;
                        tama = richTextBox1.Text.Length;
                    a = richTextBox1.Text;
                }

                }



            }
        

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* en realidad es cerrar , pero... vease comentario en cerrarToolStripMenuItem*/

            tamd = richTextBox1.Text.Length;
            d = richTextBox1.Text;
            if (!(a == d))
            /*cambios en el richtextbox*/
            //vease los comentarios en abrir ya que sigue la misma verificacion si hubo cambios
                        {

                /*sin haber guardado*/
                if (nombrearchivo == "")
                {
                    if (MessageBox.Show("¿Quieres guardar cambios a este archivo sin nombre?", "IDE",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    == DialogResult.Yes)

                    {   //se realiza guardarcomo
                        guardarComoToolStripMenuItem_Click(sender, e);
                        if (MessageBox.Show("¿Quieres cerrar  la aplicacíon?", "IDE",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        == DialogResult.Yes)
                        {
                            // como decidio cerrar ahora el richtextbox se ocultara, el texto ya no tendra nada y nombrearchivo, rutaynombre ya no tendran, a y d son iguales sin ningun string puesto a que no hay string
                            this.Text = "sin nombre";

                            richTextBox1.Text = "";
                            richTextBox1.Hide();
                            rutaynom = "";
                            nombrearchivo = "";
                            tama = 0;
                            a = "";
                            d = "";
                           //deshabilitamos los botones que por logica ya  no podrian manipular 
                            salirToolStripMenuItem.Enabled = false;
                            guardarComoToolStripMenuItem.Enabled = false;
                            guardarToolStripMenuItem.Enabled = false;
                            edicionToolStripMenuItem.Enabled = false;
                        }

                    }
                    else
                    {
                        MessageBox.Show("ok");
                        if (MessageBox.Show("¿Quieres cerrar de la aplicacíon?", "IDE",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        == DialogResult.Yes)
                        {
                            this.Text = "sin nombre";

                            richTextBox1.Text = "";
                            richTextBox1.Hide();
                            rutaynom = "";
                            nombrearchivo = "";
                            tama = 0;
                            a = "";
                            d = "";
                            salirToolStripMenuItem.Enabled = false;
                            guardarComoToolStripMenuItem.Enabled = false;
                            guardarToolStripMenuItem.Enabled = false;
                            edicionToolStripMenuItem.Enabled = false;

                        }
                    }



                }
                /*con nombre*/

                else {

                    if (MessageBox.Show("¿Quieres guardar cambios a " + rutaynom + "?", "IDE",
          MessageBoxButtons.YesNo, MessageBoxIcon.Question)
          == DialogResult.Yes)

                    {

                        SaveFileDialog guardarcomo = new SaveFileDialog();
                        guardarcomo.FileName = rutaynom;
                        richTextBox1.SaveFile(guardarcomo.FileName, RichTextBoxStreamType.PlainText);
                        tama = richTextBox1.Text.Length;
                        a = richTextBox1.Text;
                        if (MessageBox.Show("¿Quieres cerrar de la aplicacíon?", "IDE",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        == DialogResult.Yes)
                        {
                            this.Text = "sin nombre";

                            richTextBox1.Text = "";
                            richTextBox1.Hide();
                            rutaynom = "";
                            nombrearchivo = "";
                            tama = 0;
                            a = "";
                            d = "";
                            salirToolStripMenuItem.Enabled = false;
                            guardarComoToolStripMenuItem.Enabled = false;
                            guardarToolStripMenuItem.Enabled = false;
                            edicionToolStripMenuItem.Enabled = false;
                        }



                    }
                    else {
                        MessageBox.Show("ok");
                        if (MessageBox.Show("¿Quieres cerrar la aplicacíon?", "IDE",
           MessageBoxButtons.YesNo, MessageBoxIcon.Question)
           == DialogResult.Yes)
                        {
                            this.Text = "sin nombre";

                            richTextBox1.Text = "";
                            richTextBox1.Hide();
                            rutaynom = "";
                            nombrearchivo = "";
                            tama = 0;
                            a = "";
                            d = "";
                            salirToolStripMenuItem.Enabled = false;
                            guardarComoToolStripMenuItem.Enabled = false;
                            guardarToolStripMenuItem.Enabled = false;
                            edicionToolStripMenuItem.Enabled = false;
                        }

                    }

                }



                }
            else { 

                        if (MessageBox.Show("¿Quieres cerrar la aplicacíon?", "IDE",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        == DialogResult.Yes)
            {

                        richTextBox1.Text = "";
                        richTextBox1.Hide();
                        rutaynom = "";
                        nombrearchivo = "";
                        tama = 0;
                        a = "";
                        d = "";
                    salirToolStripMenuItem.Enabled = false;
                    guardarComoToolStripMenuItem.Enabled= false;
                    guardarToolStripMenuItem.Enabled = false;
                    edicionToolStripMenuItem.Enabled = false;

                }


            }

        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* en realidad es salir pero es una buena forma de evitar copy-paste, no, no confundi todo el tiempo salir con cerrar, para nada*/
            tamd = richTextBox1.Text.Length;
            d = richTextBox1.Text;
            if (!(a == d))
            /*cambios en el richbox*/

            {
              

                /*sin haber guardado*/
                if (nombrearchivo == "")
                {
                    if (MessageBox.Show("¿Quieres guardar cambios a este archivo sin nombre?", "IDE",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    == DialogResult.Yes)

                    {
                        guardarComoToolStripMenuItem_Click(sender, e);
                        if (MessageBox.Show("¿Quieres salir de la aplicacíon?", "IDE",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        == DialogResult.Yes)
                        {
                            //realiza los mismos procesos que en cerrar, pero al final te cierra el form
                            this.Text = "sin nombre";

                            richTextBox1.Text = "";
                            richTextBox1.Hide();
                            rutaynom = "";
                            nombrearchivo = "";
                            tama = 0;
                            a = "";
                            d = "";
                            Application.Exit();
                        }

                    }
                    else
                    {
                        MessageBox.Show("ok");
                        if (MessageBox.Show("¿Quieres salir de la aplicacíon?", "IDE",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        == DialogResult.Yes)
                        {
                            this.Text = "sin nombre";

                            richTextBox1.Text = "";
                            richTextBox1.Hide();
                            rutaynom = "";
                            nombrearchivo = "";
                            tama = 0;
                            a = "";
                            d = "";
                            Application.Exit();
                        }

                        MessageBox.Show("no quiste salir");
                    }



                }
                /*con nombre*/

                else {

                    if (MessageBox.Show("¿Quieres guardar cambios a " + rutaynom + "?", "IDE",
          MessageBoxButtons.YesNo, MessageBoxIcon.Question)
          == DialogResult.Yes)

                    {

                        SaveFileDialog guardarcomo = new SaveFileDialog();
                        guardarcomo.FileName = rutaynom;
                        richTextBox1.SaveFile(guardarcomo.FileName, RichTextBoxStreamType.PlainText);
                        tama = richTextBox1.Text.Length;
                        a = richTextBox1.Text;
                        if (MessageBox.Show("¿Quieres salir de la aplicacíon?", "IDE",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        == DialogResult.Yes)
                        {
                            this.Text = "sin nombre";

                            richTextBox1.Text = "";
                            richTextBox1.Hide();
                            rutaynom = "";
                            nombrearchivo = "";
                            tama = 0;
                            a = "";
                            d = "";
                            Application.Exit();
                        }
                        else {
                            MessageBox.Show("no quiste salir");
                        }


                    }
                    else {
                        MessageBox.Show("ok");
                        if (MessageBox.Show("¿Quieres salir de la aplicacíon?", "IDE",
           MessageBoxButtons.YesNo, MessageBoxIcon.Question)
           == DialogResult.Yes)
                        {
                            this.Text = "sin nombre";

                            richTextBox1.Text = "";
                            richTextBox1.Hide();
                            rutaynom = "";
                            nombrearchivo = "";
                            tama = 0;
                            a = "";
                            d = "";
                            Application.Exit();
                        }
                        else {
                          
                        }
                    }

                }



            }
            else {


              

                if (MessageBox.Show("¿Quieres salir de la aplicacíon?", "IDE",
MessageBoxButtons.YesNo, MessageBoxIcon.Question)
== DialogResult.Yes)
                {

                    richTextBox1.Text = "";
                    richTextBox1.Hide();
                    rutaynom = "";
                    nombrearchivo = "";
                    tama = 0;
                    a = "";
                    d = "";
                    salirToolStripMenuItem.Enabled = false;
                    guardarComoToolStripMenuItem.Enabled = false;
                    guardarToolStripMenuItem.Enabled = false;
                    edicionToolStripMenuItem.Enabled = false;
                    Application.Exit();
                }
                else {

                    MessageBox.Show("no quiste salir");
                }

            }

        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
            
           
        }

        private void cortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                richTextBox1.Cut();
            
        }

        private void pegarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();

        }

        private void richTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void r(object sender, EventArgs e)
        {

        }

        private void clicDer(object sender, MouseEventArgs e)
        {
            // con clic derecho podremos ver copiar,cortar y pegar , si no hay nada seleccionado no te permitira copiar o cortar
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {


                if (richTextBox1.SelectionLength < 1)
                {

                    copiarToolStripMenuItem1.Enabled = false;
                    cortar.Enabled = false;
                }
                else
                {

                    copiarToolStripMenuItem1.Enabled = true;
                    cortar.Enabled = true;
                }

                contextMenuStrip1.Show(richTextBox1, new Point(e.X, e.Y));



            }
        }

   

        private void pegarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
       
                richTextBox1.Paste();
        }

        private void cortar_Click(object sender, EventArgs e)  
        {
            if (richTextBox1.SelectionLength<1)
                MessageBox.Show("no haz seleccionado nada");
           
            else
                MessageBox.Show("seleccionado ");
            richTextBox1.Cut();
        }

        private void copiarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
                richTextBox1.Copy();
       
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
      

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // mismo proceso que en salir
            tamd = richTextBox1.Text.Length;
            d = richTextBox1.Text;
            if (!(a == d))
            /*cambios en el richbox*/

            {
                

                /*sin haber guardado*/
                if (nombrearchivo == "")
                {
                    if (MessageBox.Show("¿Quieres guardar cambios a este archivo sin nombre?", "IDE",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    == DialogResult.Yes)

                    {
                        guardarComoToolStripMenuItem_Click(sender, e);
                        

                    }
                    else
                    {
                        MessageBox.Show("ok");
                    }



                }
                /*con nombre*/

                else {

                    if (MessageBox.Show("¿Quieres guardar cambios a " + rutaynom + "?", "IDE",
          MessageBoxButtons.YesNo, MessageBoxIcon.Question)
          == DialogResult.Yes)

                    {

                        SaveFileDialog guardarcomo = new SaveFileDialog();
                        guardarcomo.FileName = rutaynom;
                        richTextBox1.SaveFile(guardarcomo.FileName, RichTextBoxStreamType.PlainText);
                        tama = richTextBox1.Text.Length;
                        a = richTextBox1.Text;
                        


                    }
                    else {
                        MessageBox.Show("ok");
            
                    }

                }



            }
            else {


                

            }


        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            MessageBox.Show("COmp creado por Brenda Samantha Ávila De la torre\n\t   version 1 \n\t   13300022 ", "",
                 MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void shellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //toma la ruta del shell y la abre
         
            
            Process.Start("..\\..\\..\\..\\App\\App\\bin\\debug\\Interprete", rutaynom);

        }

        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // aiudaaaaaaa D:
            MessageBox.Show(" aiuuudaaaa D: ", "",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void edicionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // al momento de dar clic en edicion si no hay nada seleccionado copiar y cortar estaran deshabilitados
            if (richTextBox1.SelectionLength < 1)
            {

                copiarToolStripMenuItem.Enabled = false;
                cortarToolStripMenuItem.Enabled = false;
            }
            else
            {

                copiarToolStripMenuItem.Enabled = true;
                cortarToolStripMenuItem.Enabled = true;

            }
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void compilarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            tamd = richTextBox1.Text.Length;
            d = richTextBox1.Text;
            if (!(a == d))
            /*cambios en el richbox*/

            {
                /*sin haber guardado*/
                if (nombrearchivo == "")
                {
                    if (MessageBox.Show("¿Quieres guardar cambios a este archivo sin nombre?", "IDE",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    == DialogResult.Yes)

                    {
                        guardarComoToolStripMenuItem_Click(sender, e);
                        Process.Start("..\\..\\..\\..\\compiler\\compiler\\bin\\Debug\\compiler", rutaynom);


                    }


                    else
                    {
                        MessageBox.Show("Lo sentimos, pero debe guardar cambios");

                    }


                }
                /*con nombre*/
                else {

                    if (MessageBox.Show("¿Quieres guardar cambios a " + rutaynom + "?", "IDE",
          MessageBoxButtons.YesNo, MessageBoxIcon.Question)
          == DialogResult.Yes)

                    {
                        SaveFileDialog guardarcomo = new SaveFileDialog();
                        guardarcomo.FileName = rutaynom;
                        richTextBox1.SaveFile(guardarcomo.FileName, RichTextBoxStreamType.PlainText);
                        tama = richTextBox1.Text.Length;
                        a = richTextBox1.Text;
                        Process.Start("..\\..\\..\\..\\compiler\\compiler\\bin\\Debug\\compiler", rutaynom);


                    }
                    else {
                        MessageBox.Show("Lo sentimos, pero debe guardar cambios");
                    }

                }



            }
            else {

                if (MessageBox.Show("¿Quieres guardar cambios a este archivo sin nombre?", "IDE",
         MessageBoxButtons.YesNo, MessageBoxIcon.Question)
         == DialogResult.Yes)

                {
                    guardarComoToolStripMenuItem_Click(sender, e);
                    Process.Start("..\\..\\..\\..\\compiler\\compiler\\bin\\Debug\\compiler", rutaynom);


                }


                else
                {
                    MessageBox.Show("Lo sentimos, pero debe guardar cambios");

                }

            }



        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //vuelve a mostrar salir,guardar y edicion por si habia puesto cerrar
            salirToolStripMenuItem.Enabled = true;
            guardarComoToolStripMenuItem.Enabled = true;
            guardarToolStripMenuItem.Enabled = true;
            edicionToolStripMenuItem.Enabled = true;
            richTextBox1.Show();
            this.Text = "Sin nombre";
            tamd = richTextBox1.Text.Length;
            d = richTextBox1.Text;
            // si hubo cambios
            //mismo proceso de abrir, solo que es un nuevo archivo sin haberse nunca guardado 
            if (!(a == d))
            {

                if (nombrearchivo == "")
                {
                    if (MessageBox.Show("¿Quieres guardar cambios a este archivo sin nombre?", "IDE",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    == DialogResult.Yes)
                    {
                        MessageBox.Show("guardar como");

                        SaveFileDialog guardarcomo = new SaveFileDialog();
                        guardarcomo.AddExtension = true;

                        guardarcomo.DefaultExt = "txt";
                        guardarcomo.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";

                        if (guardarcomo.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
                           guardarcomo.FileName.Length > 0)
                        {
                            nombrearchivo = System.IO.Path.GetFileName(guardarcomo.FileName);
                            rutaynom = guardarcomo.FileName;
                            this.Text = nombrearchivo;
                            richTextBox1.SaveFile(guardarcomo.FileName, RichTextBoxStreamType.PlainText);
                            tama = richTextBox1.Text.Length;
                            a = richTextBox1.Text;
                            this.Text = "sin nombre";
                            rutaynom = "";
                            nombrearchivo = "";
                            richTextBox1.Text = "";
                            tama = 0;
                            a = "";
                            d = "";


                        }



                    }
                    else {
                        MessageBox.Show("ok");
                        this.Text = "sin nombre";
                        rutaynom = "";
                        nombrearchivo = "";
                        richTextBox1.Text = "";
                        tama = 0;
                        a = "";
                        d = "";
                    }

                }
                else { 

         
                

                 if (MessageBox.Show("¿Quieres guardar cambios a " + rutaynom + "?", "IDE",
          MessageBoxButtons.YesNo, MessageBoxIcon.Question)
          == DialogResult.Yes)
                    
                        {

                    SaveFileDialog guardarcomo = new SaveFileDialog();
                    guardarcomo.FileName = rutaynom;
                    richTextBox1.SaveFile(guardarcomo.FileName, RichTextBoxStreamType.PlainText);
                    tama = richTextBox1.Text.Length;
                    a = richTextBox1.Text;
                    this.Text = "sin nombre";
                    rutaynom = "";
                    nombrearchivo = "";
                    richTextBox1.Text = "";
                    tama = 0;
                    a = "";
                    d = "";



                }
                else {
                    MessageBox.Show("ok");
                    this.Text = "sin nombre";
                    rutaynom = "";
                    nombrearchivo = "";
                    richTextBox1.Text = "";
                    tama = 0;
                    a = "";
                    d = "";


                }
            }
            }
            else
            {

                this.Text = "sin nombre";
                rutaynom = "";
                nombrearchivo = "";
                richTextBox1.Text = "";
                tama = 0;
                a = "";
                d = "";
                 
            }

           
                }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //le puse guardarcomo porque solo copie y pegue de guardarcomo 
            SaveFileDialog guardarcomo = new SaveFileDialog();
            if (nombrearchivo.Length>0)
            {              // ya tiene ruta y nombre asi que solo guarda cambios
                guardarcomo.FileName = rutaynom;
                  richTextBox1.SaveFile(guardarcomo.FileName, RichTextBoxStreamType.PlainText);
                tama = richTextBox1.Text.Length;
                a = richTextBox1.Text;
            }
            else
            {
                // realiza el proceso de guardar como
                guardarcomo.AddExtension = true;

                guardarcomo.DefaultExt = "txt";
                guardarcomo.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";

                if (guardarcomo.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
                   guardarcomo.FileName.Length > 0)
                {
                    nombrearchivo = System.IO.Path.GetFileName(guardarcomo.FileName);
                    rutaynom = guardarcomo.FileName;
                                 
                    this.Text = nombrearchivo;
                    richTextBox1.SaveFile(guardarcomo.FileName, RichTextBoxStreamType.PlainText);
                    tama = richTextBox1.Text.Length;
                    a = richTextBox1.Text;
                }

            }
        }









    }
}
