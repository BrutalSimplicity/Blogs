import scrapy
from scrapy.spiders import CrawlSpider, Rule
from scrapy.linkextractors import LinkExtractor

class GameDataSpider(CrawlSpider):
  name = "nfl_pbp_data"
  start_urls = ["http://www.pro-football-reference.com/years/2014/games.htm"]

  # the comma following the first item is REQUIRED for single rules
  rules = (
    Rule(LinkExtractor(allow=('boxscores/\w+\.htm')), callback='parse_game_data'),
  )

  def parse_game_data(self, response):
    # uses the filename as the game id that takes the form yyyymmdd0hometeam 
    # (i.e. 201409040sea) - purely a convenience choice
    game_id = response.url[response.url.rfind('/')+1:-4]
    date = game_id[:4] + '-' + game_id[4:6] + '-' + game_id[6:8]

    play_id = 1
    for row in response.xpath('//table[@id="pbp_data"]/tbody/tr[not(contains(@class,"thead"))]'):
      play = {}
      play['game_id'] = [game_id]
      play['date'] = [date]
      play['play_id'] = [play_id]
      play['quarter'] = row.xpath('td[1]//text()').extract()
      play['time'] = row.xpath('td[2]//text()').extract()
      play['down'] = row.xpath('td[3]//text()').extract()
      play['togo'] = row.xpath('td[4]//text()').extract()
      play['location'] = row.xpath('td[5]//text()').extract()
      play['detail'] = row.xpath('td[6]//text()').extract()
      play['away_points'] = row.xpath('td[7]//text()').extract()
      play['home_points'] = row.xpath('td[8]//text()').extract()
      play_id += 1

      # sanitize extracted data
      for key in play.keys():
        
        # no empty keys
        if not play[key]:
          play[key] = ''

        # join lists with multiple elements 
        elif len(play[key]) > 1:
          play[key] = ''.join(play[key])

        else:
          play[key] = play[key][0]
    
      # yield the item so that the scrapy engine can continue 
      # processing web requests concurrently
      yield play