
library(ggplot2)
library(plotly)
library(ggimage)

# --- Duomenų nuskaitymas ir paruošimas ---
df <- read.csv("publishers.csv", stringsAsFactors = FALSE)
names(df) <- make.names(names(df))

# Agreguojame pagal žanrą
df_genre <- aggregate(
  cbind(daily.average.units.sold, daily.average.gross.sales) ~ genre,
  data = df,
  FUN = sum
)

# Linijiniam grafikui – rikiuojame ir skaičiuojame kumuliatyvią sumą
df_line <- df_genre[order(-df_genre$daily.average.gross.sales), ]
df_line$CumSales <- cumsum(df_line$daily.average.gross.sales)
df_line$Index <- 1:nrow(df_line)

# Skritulinei diagramai – top 5 žanrai pagal pardavimus
df_pie <- head(df_genre[order(-df_genre$daily.average.gross.sales), ], 5)

# Bubble diagramai – vidurkiai pagal žanrą ir leidėjo tipą
df_bubble <- aggregate(
  cbind(statistics.sale.price, statistics.average.rating, daily.average.units.sold) ~
    genre + publisher.type,
  data = df,
  FUN = mean
)

# Paveikslėlio URL įterptiniam grafikui
df_pie$image_url <- "https://cdn-icons-png.flaticon.com/512/29/29302.png"


# ============================================================
# 1-2 GRAFIKAI – Bazinė R grafika: Taškų sklaidos diagrama
# ============================================================

# --- 1 Grafikas: Taškų sklaidos diagrama (Variacija A) ---
plot(
  df$statistics.sale.price,
  df$statistics.average.rating,
  xlab = "Kaina (EUR)",
  ylab = "Įvertinimas",
  pch  = 16,
  col  = "darkblue",
  main = "1 Grafikas: Taškų sklaidos diagrama"
)
# Komentaras: Matoma, kad kaina ir įvertinimas neturi ryškios tiesinės priklausomybės.
# Dauguma knygų kainuoja iki 10 EUR ir turi aukštą įvertinimą (virš 4.0).
# Brangesnės knygos pasiskirstę tolygiau pagal įvertinimą.

# --- 2 Grafikas: Taškų sklaidos diagrama (Log) ---
plot(
  df$statistics.sale.price,
  df$statistics.average.rating,
  xlab = "Kaina (logaritminė skalė)",
  ylab = "Įvertinimas",
  pch  = 17,
  col  = "red",
  log  = "x",
  main = "2 Grafikas: Taškų sklaidos diagrama"
)
# Komentaras: Logaritminė skalė leidžia geriau matyti pigesnių knygų pasiskirstymą.
# Pastebima, kad ir labai pigios (<1 EUR), ir brangios knygos gali turėti aukštus įvertinimus.


# ============================================================
# 3-4 GRAFIKAI – ggplot2: Linijinis grafikas
# ============================================================

# --- 3 Grafikas: Linijinis grafikas (Variacija A) ---
g1 <- ggplot(df_line, aes(x = Index, y = CumSales)) +
  geom_line(color = "blue", linewidth = 1) +
  geom_point(size = 2) +
  labs(
    title = "3 Grafikas: Kumuliatyviniai pardavimai pagal žanrą (Variacija A)",
    x     = "Žanras (rikiuotas pagal pardavimus)",
    y     = "Kumuliatyviniai pardavimai"
  ) +
  theme_minimal()
print(g1)
# Komentaras: Grafikas parodo, kad keletas populiariausių žanrų sudaro didžiąją
# pardavimų dalį. Kreivė staigiai kyla pradžioje ir pamažu lėtėja – tipinis
# „ilgosios uodegos" (long tail) pasiskirstymas.

# --- 4 Grafikas: Linijinis grafikas (Variacija B – plotas) ---
g12 <- ggplot(df_line, aes(x = Index, y = CumSales)) +
  geom_area(fill = "skyblue", alpha = 0.4) +
  geom_line(color = "darkblue", linetype = "dashed", linewidth = 1) +
  labs(
    title = "4 Grafikas: Kumuliatyviniai pardavimai pagal žanrą (Variacija B)",
    x     = "Žanras (rikiuotas pagal pardavimus)",
    y     = "Kumuliatyviniai pardavimai"
  ) +
  theme_minimal()
print(g2)
# Komentaras: Ploto vizualizacija išryškina bendrą augimo tendenciją ir vizualiai
# pabrėžia, kiek pardavimų sukaupę pirmieji žanrai lyginant su likusiaisiais.


# ============================================================
# 5-6 GRAFIKAI – plotly: Stulpelinė diagrama
# ============================================================

# --- 5 Grafikas: Stulpelinė diagrama (Variacija A – vertikali) ---
fig1 <- plot_ly(
  df_genre,
  x    = ~genre,
  y    = ~daily.average.units.sold,
  type = "bar",
  marker = list(color = "steelblue")
) %>%
  layout(
    title  = "5 Grafikas: Parduotų vienetų skaičius pagal žanrą (Variacija A)",
    xaxis  = list(title = "Žanras"),
    yaxis  = list(title = "Vidutin. dienos pardavimai")
  )
fig1
# Komentaras: Aiškiai matyti, kad „genre fiction" žanras dominuoja pardavimų kiekiu.
# Tarp kitų žanrų pardavimai pasiskirstę gana tolygiai, be ryškių išskirčių.

# --- 6 Grafikas: Stulpelinė diagrama (Variacija B – horizontali) ---
fig2 <- plot_ly(
  df_genre,
  x           = ~daily.average.units.sold,
  y           = ~reorder(genre, daily.average.units.sold),  
  type        = "bar",
  orientation = "h",
  marker      = list(color = "coral")
) %>%
  layout(
    title  = "6 Grafikas: Parduotų vienetų skaičius pagal žanrą (Variacija B)",
    xaxis  = list(title = "Vidutin. dienos pardavimai"),
    yaxis  = list(title = "Žanras")
  )
fig2
# Komentaras: Horizontali diagrama leidžia patogiau palyginti žanrus, kai jų pavadinimai ilgi.
# Rikiavimas pagal dydį iškart parodo aiškią hierarchiją tarp žanrų.


# ============================================================
# 7-8 GRAFIKAI – ggplot2: Skritulinė diagrama
# ============================================================

# --- 7 Grafikas: Skritulinė diagrama (Variacija A) ---
g3 <- ggplot(df_pie, aes(x = "", y = daily.average.gross.sales, fill = genre)) +
  geom_bar(stat = "identity", width = 1) +
  coord_polar("y", start = 0) +
  labs(
    title = "7 Grafikas: Top 5 žanrų pajamų dalis (Variacija A)",
    fill  = "Žanras"
  ) 
   
print(g3)
# Komentaras: Skritulinė diagrama atskleidžia, kad „genre fiction" užima didžiausią
# pajamų dalį tarp penkių populiariausių žanrų. Kiti žanrai pasiskirstę gana tolygiai.

# --- 8 Grafikas: Skritulinė diagrama su įterptiniu paveikslėliu (Variacija B) ---
g4 <- ggplot(df_pie, aes(x = "", y = daily.average.gross.sales, fill = genre)) +
  geom_bar(stat = "identity", width = 1) +
  coord_polar("y", start = 0) +
  geom_image(
    aes(x = 1, y = 0, image = image_url),
    size        = 0.2,
    inherit.aes = FALSE
  ) +
  labs(
    title = "8 Grafikas: Top 5 žanrų pajamų dalis su paveikslėliu (Variacija B)",
    fill  = "Žanras"
  ) +
  theme_void()  # PATAISYTA: pridėtas theme_void()
print(g4)
# Komentaras: Knygos ikona centre sustiprina vizualinį kontekstą – iš karto aišku,
# kad diagrama vaizduoja knygų pramonės duomenis.


# ============================================================
# 9-10 GRAFIKAI – plotly: Bubble diagrama
# ============================================================

# --- 9 Grafikas: Bubble diagrama (Variacija A) ---
fig3 <- plot_ly(
  df_bubble,
  x    = ~statistics.sale.price,
  y    = ~statistics.average.rating,
  text = ~genre,
  size = ~daily.average.units.sold,
  type = "scatter",
  mode = "markers",
  marker = list(opacity = 0.7)
) %>%
  layout(
    title  = "9 Grafikas: Kaina, įvertinimas ir pardavimai (Variacija A)",
    xaxis  = list(title = "Vidutinė kaina (EUR)"),
    yaxis  = list(title = "Vidutinis įvertinimas")
  )
fig3
# Komentaras: Burbulo dydis atspindi pardavimų kiekį. Matoma, kad didžiausi pardavimai
# koncentruojasi ties vidutine kaina ir aukštu įvertinimu. Labai brangios knygos
# paprastai parduodamos mažiau.

# --- 10 Grafikas: Animuota Bubble diagrama (Variacija B) ---
fig4 <- plot_ly(
  df_bubble,
  x     = ~statistics.sale.price,
  y     = ~statistics.average.rating,
  text  = ~genre,
  size  = ~daily.average.units.sold,
  frame = ~publisher.type,
  type  = "scatter",
  mode  = "markers",
  marker = list(opacity = 0.7)
) %>%
  animation_opts(frame = 1000, redraw = TRUE) %>%
  layout(
    title  = "10 Grafikas: Animuota Bubble diagrama pagal leidėjo tipą (Variacija B)",
    xaxis  = list(title = "Vidutinė kaina (EUR)"),
    yaxis  = list(title = "Vidutinis įvertinimas")
  )
fig4
# Komentaras: Animacija leidžia palyginti skirtingų leidėjų tipų (pvz., „big five"
# vs „small/medium") elgseną rinkoje. Pastebima, kad stambūs leidėjai dažniau
# siūlo brangesnes knygas, tačiau įvertinimai tarp grupių reikšmingai nesiskiria.

