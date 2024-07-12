using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.UI;
internal static class UIColors
{
	public static Color localPurple => new Color(160, 75, 235);
	public static Color localPurpleDark => new Color(140, 65, 220);

	public static Color disabledGray => new Color(100, 100, 100);
	public static Color disabledGrayDark => new Color(80, 80, 80);

	public static Color enabledGreen => new Color(80, 230, 70);

	public static Color dangerRed => new Color(225, 50, 85);

	public static Color infoYellow => new Color(220, 200, 30);
}
