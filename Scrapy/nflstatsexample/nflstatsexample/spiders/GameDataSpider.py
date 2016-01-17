import scrapy
import re
from scrapy.spiders import CrawlSpider, Rule
from scrapy.linkextractor import linkextractor

class GameDataSpider(scrapy.Spider):
  name = "nfl_pbp"
  start_urls = ["http://www.pro-football-reference.com/years/2014/games.htm"]

  rules = (
    Rule(LinkExtractor(allow=('boxscore/.*')), callback=parse_game_data)
  )


  def parse_game_data(self, response):
    play_id = 
    boxscore = scrapy.Item()

    for row in response.xpath('//table[@id="pbp_data"]/tbody/tr[not(normalize-space(@class)="thead")]'):
      game = GameItem()
      game['quarter'] = row.xpath('td[position()=1]//text()').extract()
      game['time'] = row.xpath('td[position()=2]//text()').extract()
      game['down'] = row.xpath('td[position()=3]//text()').extract()
      game['togo'] = row.xpath('td[position()=4]//text()').extract()
      game['location'] = row.xpath('td[position()=5]//text()').extract()
      game['detail'] = row.xpath('td[position()=6]//text()').extract()
      game['away_points'] = row.xpath('td[position()=7]//text()').extract()
      game['home_points'] = row.xpath('td[position()=8]//text()').extract()

      for key in game.keys():
        if not game[key]:
          game[key] = ''
        elif len(game[key]) > 1:
          game[key] = ''.join(game[key])
        else:
          game[key] = game[key][0]
    
    return boxscore