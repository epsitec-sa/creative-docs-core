namespace Epsitec.Common.Drawing
{
	/// <summary>
	/// L'interface ICanvasEngine d�finit un moteur capable de peindre dans
	/// une surface de type Bitmap pour remplir un Canvas � une taille donn�e.
	/// </summary>
	public interface ICanvasEngine
	{
		void GetSizeAndOrigin(byte[] data, out Drawing.Size size, out Drawing.Point origin);
		void Paint(Drawing.Graphics graphics, Drawing.Size size, byte[] data);
	}
}
