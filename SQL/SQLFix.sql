--
-- Estrutura da tabela `users_stats`
--

DROP TABLE IF EXISTS `users_stats`;
CREATE TABLE `users_stats` (
  `id` int(11) unsigned NOT NULL,
  `online_seconds` int(7) NOT NULL DEFAULT '0',
  `room_visits` int(7) NOT NULL DEFAULT '0',
  `respect` int(6) NOT NULL DEFAULT '0',
  `gifts_given` int(6) NOT NULL DEFAULT '0',
  `gifts_received` int(6) NOT NULL DEFAULT '0',
  `daily_respect_points` int(1) NOT NULL DEFAULT '3',
  `daily_pet_respect_points` int(1) NOT NULL DEFAULT '3',
  `achievement_score` int(7) unsigned NOT NULL DEFAULT '0',
  `quest_id` int(10) unsigned NOT NULL DEFAULT '0',
  `quest_progress` int(10) NOT NULL DEFAULT '0',
  `favourite_group` int(11) NOT NULL DEFAULT '0',
  `tickets_answered` int(11) NOT NULL DEFAULT '0',
  `daily_competition_votes` int(11) NOT NULL DEFAULT '3'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
