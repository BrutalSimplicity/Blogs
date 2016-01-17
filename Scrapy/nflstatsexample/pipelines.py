# -*- coding: utf-8 -*-

# Define your item pipelines here
#
# Don't forget to add your pipeline to the ITEM_PIPELINES setting
# See: http://doc.scrapy.org/en/latest/topics/item-pipeline.html
from pymongo import MongoClient

class DatabasePipeline(object):

	@classmethod
	def from_crawler(self, crawler):
		settings = crawler.settings.get('MONGO_DB')
		return cls(db_uri = settings['URI'], 
							 db_name = settings['NAME'])

	def open_spider(self, spider):
		self.client = MongoClient(self.db_uri)
		self.db = self.client[self.db_name]
		self.db.drop_collection(spider.name)

	def close_spider(self, spider):
		self.client.close()

	def process_item(self, item, spider):
		db[spider.name].insert_one(dict(item))
		return item

